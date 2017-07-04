using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wappa.Framework.Model.Pessoa;
using System.Linq;
using Wappa.Framework.Model.Comum;

namespace Wappa.Api.Driver.Controllers
{
    [Produces("application/json")]
    [Route("api/Driver")]
    public class DriverController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>GET: api/Driver</example>
        /// <returns>Lista contendo todos os motoristas</returns>
        [HttpGet]
        public IEnumerable<Motorista> Get()
        {
            IList<Motorista> motoristas = new List<Motorista>();
            motoristas.Add(new Motorista { Nome="Motorista 1", Sobrenome="Sobrenome 1", Endereco=new Endereco { Rua="Rua 1" } });
            motoristas.Add(new Motorista { Nome = "Motorista 2", Sobrenome = "Sobrenome 2", Endereco = new Endereco { Rua = "Rua 2" } });
            motoristas.Add(new Motorista { Nome = "Motorista 3", Sobrenome = "Sobrenome 3", Endereco = new Endereco { Rua = "Rua 3" } });

            return motoristas.OrderBy(motorista => motorista.NomeCompleto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <example>GET: api/Driver/5</example>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST: api/Driver
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// PUT: api/Driver/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// DELETE: api/ApiWithActions/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
