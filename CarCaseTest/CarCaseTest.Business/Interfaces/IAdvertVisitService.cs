using CarCaseTest.Domain.Models;
using CarCaseTest.Domain.Models.AdvertVisits;
using System.Threading.Tasks;

namespace CarCaseTest.Business.Interfaces
{
    public interface IAdvertVisitService
    {
        Task<ServiceResult<bool>> AddVisit(AddAdvertVisitModel model);
        Task SeedDefaultData(string rootPath);
    }
}
