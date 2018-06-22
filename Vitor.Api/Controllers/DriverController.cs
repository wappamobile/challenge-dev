using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vitor.Domain.Messages;
using Vitor.Domain.Messages.Request;
using Vitor.Domain.Service;

namespace Vitor.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Driver")]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            this._driverService = driverService;
        }

        [HttpGet]
        [Route("getdriver")]
        public async Task<IActionResult> GetDriver(long id)
        {
            try
            {
                return Ok(await this._driverService.GetDriver(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR WHEN EXECUTING GET DRIVER METHOD.");
            }
        }

        [HttpGet]
        [Route("getdriverbyemail")]
        public async Task<IActionResult> Getdriverbyemail(string email)
        {
            try
            {
                return Ok(await this._driverService.Getdriverbyemail(email));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR WHEN EXECUTING GET DRIVER METHOD.");
            }
        }

        [HttpPost]
        [Route("insertdriver")]
        public async Task<IActionResult> InsertDriver([FromBody]InsertDriverRequest driver)
        {
            try
            {         
                return Ok(await this._driverService.InsertDriver(driver));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR WHEN EXECUTING INSERT DRIVER METHOD.");
            }
        }

        [HttpPut]
        [Route("updatedriver")]
        public async Task<IActionResult> UpdateDriver([FromBody]UpdateDriverRequest driver)
        {
            try
            {
                return Ok(await this._driverService.UpdateDriver(driver));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR WHEN EXECUTING PUT DRIVER METHOD.");
            }
        }

        [HttpDelete]
        [Route("deletedriver")]
        public async Task<IActionResult> DeleteDriver(long id)
        {
            try
            {
                return Ok(await this._driverService.DeleteDriver(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR WHEN EXECUTING DELETE DRIVER METHOD.");
            }
        }
    }
}
