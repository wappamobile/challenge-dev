using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;

namespace Wappa.Challenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class EnderecoController : Controller
    {
        private IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService_)
        {
            this._enderecoService = enderecoService_;
        }

        // GET api/values
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<ApplicationCore.Entities.Endereco> Listar()
        {
            return _enderecoService.Listar();
        }

        // GET api/values/5
        [HttpGet]
        [Route("Obter/{id}")]
        public ApplicationCore.Entities.Endereco Obter(long id)
        {
            return _enderecoService.Obter(id: id);
        }

        // POST api/values
        [HttpPost]
        [Route("Adicionar")]
        public ApplicationCore.Entities.Endereco Adicionar([FromBody]ApplicationCore.Entities.Endereco modelo)
        {
            return _enderecoService.Adicionar(entity: modelo);
        }

        // PUT api/values/5
        [HttpPut]
        [Route("Alterar")]
        public ApplicationCore.Entities.Endereco Alterar([FromBody]ApplicationCore.Entities.Endereco modelo)
        {
            return _enderecoService.Alterar(entity: modelo);
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("Apagar/{id}")]
        public bool Apagar(long id)
        {
            return _enderecoService.Apagar(id);
        }
    }
}
