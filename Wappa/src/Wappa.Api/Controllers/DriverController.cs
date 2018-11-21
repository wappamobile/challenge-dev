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
    /// Controla os motoristas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : CustomController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public DriverController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Adiciona um novo Motorista
        /// </summary>
        /// <param name="command">Dados do motorista</param>
        /// <returns>Motorista inserido</returns>
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IDriver), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync([FromBody] CreateDriverRequest command) =>
            await SendRouteAsync<IDriver>(
                command, "Get", (o) => { return new { o.Id }; });

        /// <summary>
        /// Altera informações do Motorista
        /// </summary>
        /// <param name="id">Código do Motorista</param>
        /// <param name="command">Dados do motorista</param>
        /// <returns>HTTP Status</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ChangeDriverRequest command) =>
            await SendAsync(command.ApplyId(id));

        /// <summary>
        /// Remove o motorista
        /// </summary>
        /// <param name="id">Código do motorista</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id) =>
            await SendAsync(new RemoveDriverRequest(id));

        /// <summary>
        /// Seleciona o motorista pelo código
        /// </summary>
        /// <param name="id">Código do motorista</param>
        /// <returns>Motorista</returns>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(IDriver), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            return await SendAsync(new QueryDriverRequest(id));
        }

        /// <summary>
        /// Seleciona motorista pelo CPF
        /// </summary>
        /// <param name="document">CPF do motorista</param>
        /// <returns>Motorista</returns>
        [HttpGet("ByDocument{document}", Name = nameof(GetByDocumentAsync))]
        [ProducesResponseType(typeof(IDriver), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByDocumentAsync(
            [FromRoute] string document) =>
            await SendAsync(new QueryDriverRequest(document));

        /// <summary>
        /// Seleciona motoristas por pesquisa
        /// </summary>
        /// <param name="id">Código do motorista</param>
        /// <param name="document">Documento do motorista</param>
        /// <param name="firstName">Primeiro nome</param>
        /// <param name="lastName">Último nome</param>
        /// <returns></returns>
        [HttpGet, Route("Search")]
        [ProducesResponseType(typeof(IEnumerable<IDriver>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBySearchAsync([FromQuery]int? id,
            [FromQuery]string document, [FromQuery]string firstName,
            [FromQuery]string lastName) =>
            await SendAsync(new QueryDriverRequest
            {
                Id = id,
                Document = document,
                FirstName = firstName,
                LastName = lastName
            });
    }
}