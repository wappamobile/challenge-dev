using Wappa.Middleware.Application.Drivers;
using Wappa.Middleware.Application.Drivers.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wappa.Middleware.Host.Controllers.Drivers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DriverController : BaseController
    {
        private readonly IDriverAppService _driverAppService;

        public DriverController(IDriverAppService driverAppService)
        {
            _driverAppService = driverAppService;
        }

        [HttpGet]

        public async Task<List<DriverDto>> GetAll()
        {
            return await _driverAppService.GetAll();
        }

        [HttpGet("{id}")]

        public async Task<DriverDto> GetById(int id)
        {
            return await _driverAppService.GetById(id);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateOrEditDriverDto input)
        {
            var result = ResultData(await _driverAppService.Create(input));
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = Result(await _driverAppService.Delete(id));
            return result;
        }

        [HttpPut()]

        public async Task<IActionResult> Update(CreateOrEditDriverDto input)
        {
            var result = Result(await _driverAppService.Update(input));
            return result;
        }
    }
}