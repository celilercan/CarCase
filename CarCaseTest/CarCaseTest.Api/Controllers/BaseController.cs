using CarCaseTest.Domain.Enums;
using CarCaseTest.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarCaseTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected ActionResult HttpResult<T>(ServiceResult<T> result)
        {
            switch (result.Status)
            {
                case ResultStatus.Success:
                    return Ok(result.Data);
                case ResultStatus.NoContent:
                    return NoContent();
                case ResultStatus.NotFound:
                    return NotFound();
                case ResultStatus.ValidationError:
                    return BadRequest();
                case ResultStatus.Error:
                    return StatusCode(StatusCodes.Status500InternalServerError);
                default:
                    return NotFound();
            }
        }
    }
}
