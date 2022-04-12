using CarCaseTest.Business.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CarCaseTest.Api.Controllers
{
    public class DataController : BaseController
    {
        private readonly IAdvertService _advertService;
        private readonly IAdvertVisitService _advertVisitService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DataController(IAdvertService advertService, IAdvertVisitService advertVisitService, IHostingEnvironment hostingEnvironment)
        {
            _advertService = advertService;
            _advertVisitService = advertVisitService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("seed")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> SeedDefaultData()
        {
            await _advertService.SeedDefaultData(_hostingEnvironment.ContentRootPath);
            await _advertVisitService.SeedDefaultData(_hostingEnvironment.ContentRootPath);
            await _advertService.IndexAllData();
            return Ok();
        }
    }
}
