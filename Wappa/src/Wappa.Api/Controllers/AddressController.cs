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
    /// Controla os endereços do motorista
    /// </summary>
    [Route("api/Driver/{driverId}/[controller]")]
    [ApiController]
    public class AddressController : CustomController
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="mediator"></param>
        public AddressController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Adiciona um novo endereço para o motorista
        /// </summary>
        /// <param name="driverId">Código do motorista</param>
        /// <param name="command">Dados do endereço</param>
        /// <returns>Retorna o motorista</returns>
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IAddress), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync([FromRoute] int driverId,
            [FromBody] CreateAddressRequest command) =>
            await SendRouteAsync<IAddress>(
                      command.ApplyDriverId(driverId),
                      "GetAddress",
                      (o) => { return new { driverId = o.DriverId }; });

        /// <summary>
        /// Muda o endereço do motorista
        /// </summary>
        /// <param name="driverId">Código do motorista</param>
        /// <param name="item">Código do item</param>
        /// <param name="command">Dados do endereço</param>
        /// <returns>HTTP status</returns>
        [HttpPut("{item}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutAsync([FromRoute] int driverId,
            [FromRoute] int item, [FromBody] ChangeAddressRequest command) =>
            await SendAsync(command.ApplyIds(driverId, item));

        /// <summary>
        /// Remove endereço do motorista
        /// </summary>
        /// <param name="driverId">Código do motorista</param>
        /// <param name="item">Código do item</param>
        /// <returns>HTTP status</returns>
        [HttpDelete("{item}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int driverId,
            [FromRoute] int item) =>
            await SendAsync(new RemoveAddressRequest(driverId, item));

        /// <summary>
        /// Seleciona os endereços do motorista
        /// </summary>
        /// <param name="driverId">Código do motorista</param>
        /// <returns>Lista com os endereços</returns>
        [HttpGet(Name = "GetAddress")]
        [ProducesResponseType(typeof(IEnumerable<IAddress>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAddressAsync([FromRoute] int driverId) =>
            await SendAsync(new QueryAddressRequest(driverId));
    }
}