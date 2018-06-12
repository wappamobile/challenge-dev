using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;

namespace Wappa.Challenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MotoristaController : Controller
    {
        private IMotoristaService _motoristaService;

        public MotoristaController(IMotoristaService motoristaService_)
        {
            this._motoristaService = motoristaService_;
        }

        /// <summary>
        /// Listar todos os motoristas
        /// </summary>
        /// <param name="ordenarPor">Parametro para ordenação. Disponivel: Nome ou Sobrenome</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<ApplicationCore.Entities.Motorista> Listar(string ordenarPor)
        {
            var model = _motoristaService.Listar();

            if (!String.IsNullOrEmpty(ordenarPor))
            {
                if (ordenarPor.ToLower() == "nome")
                    model = model.OrderBy(a => a.Nome);
                else if (ordenarPor.ToLower() == "sobrenome")
                    model = model.OrderBy(a => a.Sobrenome);
            }

            return model;
        }

        [HttpGet]
        [Route("Obter/{id}")]
        public ApplicationCore.Entities.Motorista Obter(long id)
        {
            return _motoristaService.Obter(id: id);
        }

        [HttpPost]
        [Route("Adicionar")]
        public ApplicationCore.Entities.Motorista Adicionar([FromBody]ApplicationCore.Entities.Motorista modelo)
        {
            return _motoristaService.Adicionar(entity: modelo);
        }

        [HttpPut]
        [Route("Alterar")]
        public ApplicationCore.Entities.Motorista Alterar([FromBody]ApplicationCore.Entities.Motorista modelo)
        {
            return _motoristaService.Alterar(entity: modelo);
        }

        [HttpDelete]
        [Route("Apagar/{id}")]
        public bool Apagar(long id)
        {
            return _motoristaService.Apagar(id);
        }
    }
}
