using System;
using DriverRegistration.Application;
using DriverRegistration.Application.DTOs.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DriverRegistration.API.Controllers
{
    [Produces("application/json")]
    [Route("api/car")]
    public class CarController : Controller
    {
        private IConfiguration _configuration;

        public CarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Carrega um determinado Carro.
        /// </summary>
        /// <param name="id">Id do Carro</param>
        /// <returns>Retorna um objeto Carro</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CarResponse))]
        public IActionResult Get(int id)
        {
            if (ModelState.IsValid)
            {
                ApplicationCar application = new ApplicationCar(_configuration);                
                return Ok(application.Load(id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Realiza a inclusão de um novo Carro.
        /// </summary>
        /// <param name="request">Objeto Carro a ser incluído.</param>
        /// <returns>Retorna o próprio Objeto Carro com ID</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CarResponse))]
        public IActionResult Post([FromBody]CarPostRequest request)
        {
            if (ModelState.IsValid)
            {
                ApplicationCar application = new ApplicationCar(_configuration);                
                return Ok(application.Add(request));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza um determinado Carro.
        /// </summary>
        /// <param name="id">Id do carro a ser atualizado.</param>
        /// <param name="request">Objeto carro a ser atualizaado.</param>
        /// <returns>retorna Verdadeiro ou falso</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Boolean))]
        public IActionResult Put(int id, [FromBody]CarPutRequest request)
        {
            if (ModelState.IsValid)
            {
                ApplicationCar application = new ApplicationCar(_configuration);
                return Ok(application.Update(request));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Exclusão de um determinado Carro.
        /// </summary>
        /// <param name="id">Id do Carro a ser excluído.</param>
        /// <returns>retorna Verdadeiro ou falso</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Boolean))]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                ApplicationCar application = new ApplicationCar(_configuration);                
                return Ok(application.Delete(id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
