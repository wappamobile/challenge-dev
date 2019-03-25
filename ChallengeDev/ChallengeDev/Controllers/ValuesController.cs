using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadastro.Entities;
using Cadastro.Interface;
using Cadastro.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChallengeDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICadastroModel _cadastro =  new CadastroModel();

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            var motoristas = JsonConvert.SerializeObject(_cadastro.RetornaTodos());
            return new JsonResult(motoristas);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Motorista novoMotorista)
        {
            _cadastro.NovoCadastro(novoMotorista);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
           
        }

        [HttpPost]
        public bool Teste()
        {
            return true;
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
