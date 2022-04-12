using CarCaseTest.Business.Interfaces;
using CarCaseTest.Business.Mapping;
using CarCaseTest.Domain.Entities;
using CarCaseTest.Domain.Models;
using CarCaseTest.Domain.Models.AdvertVisits;
using CarCaseTest.Infrastructure.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace CarCaseTest.Business.Services
{
    public class AdvertVisitService : IAdvertVisitService
    {
        private readonly IAdvertVisitRepository _advertVisitRepository;
        private readonly IConfiguration _configuration;

        public AdvertVisitService(IAdvertVisitRepository advertVisitRepository, IConfiguration configuration)
        {
            _advertVisitRepository = advertVisitRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResult<bool>> AddVisit(AddAdvertVisitModel model)
        {
            var result = new ServiceResult<bool>();
            var data = DtoMapper.Mapper.Map<AdvertVisitHistory>(model);
            await _advertVisitRepository.AddAsync(data);
            result.Data = true;
            return result;
        }

        public async Task SeedDefaultData(string rootPath)
        {
            var count = await _advertVisitRepository.AllCount();
            if (count > default(int))
                return;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlserver")))
            {
                connection.Open();
                var insertQuery = File.ReadAllText(Path.Combine(rootPath, "ProjectFiles/AdvertVisit.sql"));
                await connection.ExecuteAsync(insertQuery);
            }
        }
    }
}
