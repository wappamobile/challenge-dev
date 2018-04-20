using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Domain.Entidades;
using Wappa.Domain.Services;

namespace Wappa.API.Controllers
{
    [Route("api/[controller]")]
    public class MotoristaController : Controller
    {
        private readonly IMotoristaService motoristaService;

        public MotoristaController(IMotoristaService motoristaService)
        {
            this.motoristaService = motoristaService;
        }

        [HttpGet("[action]")]
        public Motorista ObterPorId(int motoristaID)
        {
            return this.motoristaService.ObterPorId(motoristaID);
        }

        [HttpGet("[action]")]
        public IEnumerable<Motorista> ObterOrdenadoPorPrimeiroNome()
        {
            return this.motoristaService.ObterOrdenadoPorPrimeiroNome();
        }

        [HttpGet("[action]")]
        public IEnumerable<Motorista> ObterOrdenadoPorUltimoNome()
        {
            return this.motoristaService.ObterOrdenadoPorUltimoNome();
        }

        [HttpPost("[action]")]
        public Motorista Novo([FromBody] Motorista motorista)
        {
            this.motoristaService.Novo(motorista);            

            return motorista;
        }

        [HttpPost("[action]")]
        public Motorista Alterar([FromBody] Motorista motorista)
        {
            this.motoristaService.Alterar(motorista);

            return motorista;
        }

        [HttpDelete("[action]")]
        public void Excluir(int motoristaID)
        {
            this.motoristaService.Excluir(motoristaID);
        }
    }
}
