using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Domain.DriverAggregation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddDriverDto dto,
            [FromServices] AddDriverSpecification specification,
            [FromServices] ICordinatesService cordinatesService, 
            [FromServices] IDriverRepository repository)
        {
            var validation = await specification.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var entity = new Driver(dto, cordinatesService);
            await repository.Add(entity);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDriverDto dto,
            [FromServices] UpdateDriverSpecification specification,
            [FromServices] ICordinatesService cordinatesService,
            [FromServices] IDriverRepository repository)
        {
            var validation = await specification.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var entity = await repository.GetById(dto);
            entity.Update(dto, cordinatesService);
            await repository.Add(entity);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] RemoveDriverDto dto,
            [FromServices] RemoveDriverSpecification specification,
            [FromServices] ICordinatesService cordinatesService,
            [FromServices] IDriverRepository repository)
        {
            var validation = await specification.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var entity = await repository.GetById(dto);
            await repository.Remove(entity);
            return Ok();
        }

    }
}