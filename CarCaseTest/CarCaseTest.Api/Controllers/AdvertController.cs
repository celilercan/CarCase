using CarCaseTest.Business.Interfaces;
using CarCaseTest.Domain.Models.Adverts;
using CarCaseTest.Domain.Models.AdvertVisits;
using CarCaseTest.Queue.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CarCaseTest.Api.Controllers
{
    public class AdvertController : BaseController
    {
        private readonly IAdvertService _advertService;
        private readonly IBus _bus;

        public AdvertController(IAdvertService advertService, IBus bus)
        {
            _advertService = advertService;
            _bus = bus;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(AdvertListResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] AdvertSearchFilterModel filter)
        {
            var result = await _advertService.SearchAsync(filter);
            return HttpResult(result);
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(AdvertListResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _advertService.GetAdvertAsync(id);
            return HttpResult(result);
        }

        [HttpPost("visit")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Visit([FromBody] AdvertVisitRequestModel model)
        {
            var message = new AdvertVisitMessage { AdvertId = model.AdvertId, IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(), VisitDate = DateTime.Now };
            await _bus.Publish(message);
            return Created("advert/visit", "Visit created");
        }
    }
}
