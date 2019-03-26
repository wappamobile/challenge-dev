using Cadastro.Entities;
using Cadastro.Interface;
using Cadastro.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChallengeDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        private readonly ICadastroModel _cadastro =  new CadastroModel();
        private readonly IBuscaCoordenadas _buscaCoordenadas;

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            var motoristas = JsonConvert.SerializeObject(_cadastro.RetornaTodos());
            return new JsonResult(motoristas);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Motorista> Get(int id)
        {
            return _cadastro.RetornaPorId(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Cadastro([FromBody] Motorista novoMotorista)
        {
            novoMotorista.Endereco = _buscaCoordenadas.RetornaEnderecoComCoordenadas(novoMotorista.Endereco);
            _cadastro.NovoCadastro(novoMotorista);
            return new JsonResult("Success");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody] Motorista motorista)
        {
            _cadastro.AtualizaCadastro(motorista);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _cadastro.DeleteCadastro(id);
        }
    }
}
