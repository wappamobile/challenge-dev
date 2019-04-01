using Wappa.Middleware.Application.Cars;
using Wappa.Middleware.Application.Cars.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wappa.Middleware.Host.Controllers.Cars
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : BaseController
    {
        private readonly ICarAppService _carAppService;

        public CarController(ICarAppService carAppService)
        {
            _carAppService = carAppService;
        }

        [HttpGet]

        public async Task<List<CarDto>> GetAll()
        {
            return await _carAppService.GetAll();
        }

        [HttpGet("{id}")]
       
        public async Task<CarDto> GetById(int id)
        {
            return await _carAppService.GetById(id);
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(CreateOrEditCarDto input)
        {
            var result = ResultData(await _carAppService.Create(input));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = Result(await _carAppService.Delete(id));
            return result;
        }

        [HttpPut()]
        
        public async Task<IActionResult> Update(CreateOrEditCarDto input)
        {
            var result = Result(await _carAppService.Update(input));
            return result;
        }
    }
}