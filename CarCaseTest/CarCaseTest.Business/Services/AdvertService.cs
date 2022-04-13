using CarCaseTest.Business.Interfaces;
using CarCaseTest.Business.Mapping;
using CarCaseTest.Business.Search;
using CarCaseTest.Business.Search.IndexModels;
using CarCaseTest.Domain.Enums;
using CarCaseTest.Domain.Models;
using CarCaseTest.Domain.Models.Adverts;
using CarCaseTest.Infrastructure.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarCaseTest.Business.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly ElasticSearchManager _elasticSearchManager;
        private readonly IConfiguration _configuration;

        public AdvertService(IAdvertRepository advertRepository, ElasticSearchManager elasticSearchManager, IConfiguration configuration)
        {
            _advertRepository = advertRepository;
            _elasticSearchManager = elasticSearchManager;
            _configuration = configuration;
        }

        public async Task<ServiceResult<AdvertDetailModel>> GetAdvertAsync(int id)
        {
            var result = new ServiceResult<AdvertDetailModel>();
            var entity = await _advertRepository.GetByIdAsync(id);
            if (entity == null)
            {
                result.Status = ResultStatus.NoContent;
                return result;
            }

            result.Data = DtoMapper.Mapper.Map<AdvertDetailModel>(entity);
            return result;
        }

        public async Task IndexAllData()
        {
            var data = await _advertRepository.GetAllAsync();
            var indexData = data.Select(x => DtoMapper.Mapper.Map<AdvertListIndex>(x)).ToList();
            _elasticSearchManager.CreateIndex();
            _elasticSearchManager.IndexMany(indexData);
        }

        public async Task<ServiceResult<AdvertListResultModel>> SearchAsync(AdvertSearchFilterModel filter)
        {
            var result = new ServiceResult<AdvertListResultModel>();
            var searchResponse = await _elasticSearchManager.SearchAdvert(filter);
            result.Data = DtoMapper.Mapper.Map<AdvertListResultModel>(searchResponse);
            result.Data.Page = filter.Page.GetValueOrDefault();
            result.Status = searchResponse.Total > default(int) ? ResultStatus.Success : ResultStatus.NoContent;
            return result;
        }

        public async Task SeedDefaultData(string rootPath)
        {
            var count = await _advertRepository.AllCount();
            if (count > default(int))
                return;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlserver")))
            {
                connection.Open();
                var insertQuery = File.ReadAllText(Path.Combine(rootPath, "ProjectFiles/Advert.sql"));
                await connection.ExecuteAsync(insertQuery);
            }
        }
    }
}
