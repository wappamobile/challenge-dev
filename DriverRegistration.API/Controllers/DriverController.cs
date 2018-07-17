using System.Collections.Generic;
using DriverRegistration.Application.DTOs.Driver;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DriverRegistration.Application;

namespace DriverRegistration.API.Controllers
{
    [Produces("application/json")]
    [Route("api/driver")]
    public class DriverController : Controller
    {
        private IConfiguration _configuration;

        public DriverController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Lista os Motoristas cadastrados ordenadamente.
        /// </summary>
        /// <param name="rowindex">Linha inicial da paginação.</param>
        /// <param name="rowget">Quantidade de linhas que serão retornadas.</param>
        /// <param name="direction">Ordenação: 0 - Pelo Primeiro Nome, 1 - Pelo Sobrenome</param>
        /// <returns>Retorna uma lista de Motoristas.</returns>
        [HttpGet("{rowindex}/{rowget}/{direction}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DriverResponse>))]
        public IActionResult Get(int rowindex, int rowget, int direction)
        {
            if (ModelState.IsValid)
            {
                ApplicationDriver application = new ApplicationDriver(_configuration);
                return Ok(application.Get(rowindex, rowget, direction));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Carrega um determinado Motorista.
        /// </summary>
        /// <param name="id">Id do Motorista.</param>
        /// <returns>Retorna um objeto Motorista.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(DriverResponse))]
        public IActionResult Get(int id)
        {
            if (ModelState.IsValid)
            {
                ApplicationDriver application = new ApplicationDriver(_configuration);
                return Ok(application.Load(id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Realiza a inclusão de um novo Motorista.
        /// </summary>
        /// <param name="request">Objeto Motorista a ser incluído.</param>
        /// <returns>Retorna o próprio objeto Motorista com o Id Preenchido.</returns>
        [HttpPost]
        public IActionResult Post([FromBody]DriverPostRequest request)
        {
            if (ModelState.IsValid)
            {
                ApplicationDriver application = new ApplicationDriver(_configuration);
                return Ok(application.Add(request));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza um determinado Motorista.
        /// </summary>
        /// <param name="id">Id do motorista a ser atualizado.</param>
        /// <param name="request">Objeto motorista a ser atualizaado.</param>
        /// <returns>retorna Verdadeiro ou falso</returns>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DriverPutRequest request)
        {
            if (ModelState.IsValid)
            {
                ApplicationDriver application = new ApplicationDriver(_configuration);
                return Ok(application.Update(request));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Exclusão de um determinado Motorista.
        /// </summary>
        /// <param name="id">Id do Motorista a ser excluído.</param>
        /// <returns>retorna Verdadeiro ou falso</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                ApplicationDriver application = new ApplicationDriver(_configuration);
                return Ok(application.Delete(id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
