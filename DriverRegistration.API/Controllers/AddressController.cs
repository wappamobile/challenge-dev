using System;
using DriverRegistration.Application;
using DriverRegistration.Application.DTOs.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverRegistration.API.Controllers
{
    [Produces("application/json")]
    [Route("api/address")]
    public class AddressController : Controller
    {
        private IConfiguration _configuration;

        public AddressController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Carrega um determinado Endereço.
        /// </summary>
        /// <param name="id">Id do Endereço.</param>
        /// <returns>Retorna um Objeto Endereço.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AddressResponse))]
        public IActionResult Get(int id)
        {
            if (ModelState.IsValid)
            {
                ApplicationAddress application = new ApplicationAddress(_configuration);                
                return Ok(application.Load(id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Realiza a inclusão de um novo Endereço.
        /// </summary>
        /// <param name="request">Objeto Endereço a ser incluído.</param>
        /// <returns>Retorna o próprio objeto Endereço com ID.</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AddressResponse))]
        public IActionResult Post([FromBody]AddressPostRequest request)
        {
            if (ModelState.IsValid)
            {
                ApplicationAddress application = new ApplicationAddress(_configuration);                
                return Ok(application.Add(request));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza um determinado Endereço.
        /// </summary>
        /// <param name="id">Id do Endereço a ser atualizado.</param>
        /// <param name="request">Objeto Endereço a ser atualizado.</param>
        /// <returns>retorna Verdadeiro ou falso.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Boolean))]
        public IActionResult Put(int id, [FromBody]AddressPutRequest request)
        {
            if (ModelState.IsValid)
            {
                ApplicationAddress application = new ApplicationAddress(_configuration);
                return Ok(application.Update(request));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Exclusão de um determinado Endereço.
        /// </summary>
        /// <param name="id">Id do Endereço a ser excluído.</param>
        /// <returns>retorna Verdadeiro ou falso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Boolean))]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                ApplicationAddress application = new ApplicationAddress(_configuration);
                return Ok(application.Delete(id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
