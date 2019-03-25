using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WappaMobile.Application;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using WappaMobile.Domain;

namespace WappaMobile.WebAPI.Controllers
{
    [Route("api/v1/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriversController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Lists all registered drivers.
        /// </summary>
        /// <returns>The ordered list of registered drivers.</returns>
        /// <param name="orderBy">"last" for LastName, otherwise FirstName will be used.</param>
        /// <response code="200">The ordered list of registered drivers.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ViewDriverDto>>> ListAsync(string orderBy)
        {
            var sorting = orderBy == "last" ?
                ListDriversQuery.Sorting.LastName :
                ListDriversQuery.Sorting.FirstName;

            var query = new ListDriversQuery(sorting);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Returns a single driver.
        /// </summary>
        /// <returns>The requested driver information.</returns>
        /// <param name="driverId">The driver identifier.</param>
        /// <response code="200">The requested driver information.</response>
        /// <response code="404">If the driver is not found.</response>
        [HttpGet("{driverId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ViewDriverDto>> GetAsync(Guid driverId)
        {
            var query = new GetDriverQuery(driverId);

            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new driver.
        /// </summary>
        /// <param name="driverInfo">The driver information.</param>
        /// <response code="200">If the driver is successfully created.</response>
        /// <response code="400">If the driver information is invalid.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> PostAsync([FromBody] ModifyDriverDto driverInfo)
        {
            var command = new CreateDriverCommand(driverInfo);

            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Updates a registered driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <param name="driverInfo">The driver information.</param>
        /// <response code="200">If the driver is successfully modified.</response>
        /// <response code="400">If the driver information is invalid.</response>
        /// <response code="404">If the driver is not found.</response>
        [HttpPut("{driverId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> PutAsync(Guid driverId, [FromBody] ModifyDriverDto driverInfo)
        {
            var command = new UpdateDriverCommand(driverId, driverInfo);

            try
            {
                await _mediator.Send(command);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a driver.
        /// </summary>
        /// <param name="driverId">The driver identifier.</param>
        /// <response code="200">If the driver is successfully deleted.</response>
        /// <response code="404">If the driver is not found.</response>
        [HttpDelete("{driverId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAsync(Guid driverId)
        {
            var command = new DeleteDriverCommand(driverId);

            try
            {
                await _mediator.Send(command);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
