using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;

namespace Wappa.Challenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CarroController : Controller
    {
        private ICarroService _carroService;

        public CarroController(ICarroService carroService_)
        {
            this._carroService = carroService_;
        }

        // GET api/values
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<ApplicationCore.Entities.Carro> Listar()
        {
            return _carroService.Listar();
        }

        // GET api/values/5
        [HttpGet]
        [Route("Obter/{id}")]
        public ApplicationCore.Entities.Carro Obter(long id)
        {
            return _carroService.Obter(id: id);
        }

        // POST api/values
        [HttpPost]
        [Route("Adicionar")]
        public ApplicationCore.Entities.Carro Adicionar([FromBody]ApplicationCore.Entities.Carro modelo)
        {
            return _carroService.Adicionar(entity: modelo);
        }

        // PUT api/values/5
        [HttpPut]
        [Route("Alterar")]
        public ApplicationCore.Entities.Carro Alterar([FromBody]ApplicationCore.Entities.Carro modelo)
        {
            return _carroService.Alterar(entity: modelo);
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("Apagar/{id}")]
        public bool Apagar(long id)
        {
            return _carroService.Apagar(id);
        }
    }
}
