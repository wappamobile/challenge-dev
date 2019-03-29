using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Domain.DriverAggregation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace Challenge.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ValidationFailure),400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Add([FromBody] AddDriverDto dto,
            [FromServices] AddDriverSpecification specification,
            [FromServices] IGeocodingService geocodingService, 
            [FromServices] IDriverRepository repository)
        {
            var validation = await specification.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var entity = new Driver(dto, geocodingService);
            await repository.Add(entity);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(ValidationFailure), 400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateDriverDto dto,
            [FromServices] UpdateDriverSpecification specification,
            [FromServices] IGeocodingService cordinatesService,
            [FromServices] IDriverRepository repository)
        {
            var validation = await specification.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var entity = await repository.GetById(dto);
            entity.Update(dto, cordinatesService);
            await repository.Update(entity);
            return Ok();
        }


        [HttpDelete]
        [ProducesResponseType(typeof(ValidationFailure), 400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Remove([FromQuery] RemoveDriverDto dto,
            [FromServices] RemoveDriverSpecification specification,
            [FromServices] IGeocodingService cordinatesService,
            [FromServices] IDriverRepository repository)
        {
            var validation = await specification.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var entity = await repository.GetById(dto);
            await repository.Remove(entity);
            return Ok();
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Driver>), 200)]
        public async Task<IActionResult> Get([FromQuery]GetDriversDto dto,
            [FromServices] IDriverRepository repository)
        {
            return Ok(await repository.Get(dto.Order));
        }
    }
}