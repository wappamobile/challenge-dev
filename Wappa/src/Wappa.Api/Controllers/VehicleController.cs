using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Interfaces.Models;

namespace Wappa.Api.Controllers
{
    /// <summary>
    /// Controlador do veículo
    /// </summary>
    [Route("api/Driver/{driverId}/[controller]")]
    [ApiController]
    public class VehicleController : CustomController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public VehicleController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Adiciona um novo veículo ao Motorista
        /// </summary>
        /// <param name="driverId">Código do Motorista</param>
        /// <param name="command">Dados do veículo</param>
        /// <returns>Retorna o veículo</returns>
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IVehicle), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync([FromRoute] int driverId,
            [FromBody] CreateVehicleRequest command) =>
            await SendRouteAsync<IVehicle>(
                command.ApplyDriverId(driverId),
                "GetVehicle",
                (o) => { return new { driverId = o.DriverId }; });

        /// <summary>
        /// Altera dados do veículo do motorista
        /// </summary>
        /// <param name="driverId">Código do motorista</param>
        /// <param name="item">Código do veículo</param>
        /// <param name="command">Dados de alteração</param>
        /// <returns>HTTP STATUS</returns>
        [HttpPut("{item}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutAsync([FromRoute] int driverId,
            [FromRoute] int item, [FromBody] ChangeVehicleRequest command) =>
            await SendAsync(command.ApplyIds(driverId, item));

        /// <summary>
        /// Remove o veículo do motorista
        /// </summary>
        /// <param name="driverId">Código do motorista</param>
        /// <param name="item">Código do veículo</param>
        /// <returns>HTTP STATUS</returns>
        [HttpDelete("{item}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int driverId,
            [FromRoute] int item) =>
            await SendAsync(new RemoveVehicleRequest(driverId, item));

        /// <summary>
        /// Selecione os veículos do motorista
        /// </summary>
        /// <param name="driverId">Código do motorista</param>
        /// <returns>Lista de veículos</returns>
        [HttpGet(Name = "GetVehicle")]
        [ProducesResponseType(typeof(IEnumerable<IVehicle>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetVehicleAsync([FromRoute] int driverId) =>
            await SendAsync(new QueryVehicleRequest(driverId));
    }
}