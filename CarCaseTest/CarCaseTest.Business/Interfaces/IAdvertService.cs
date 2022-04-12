using CarCaseTest.Business.Search.IndexModels;
using CarCaseTest.Domain.Models;
using CarCaseTest.Domain.Models.Adverts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCaseTest.Business.Interfaces
{
    public interface IAdvertService
    {
        Task SeedDefaultData(string rootPath);

        Task IndexAllData();

        Task<ServiceResult<AdvertListResultModel>> SearchAsync(AdvertSearchFilterModel filter);

        Task<ServiceResult<AdvertDetailModel>> GetAdvertAsync(int id);
    }
}
