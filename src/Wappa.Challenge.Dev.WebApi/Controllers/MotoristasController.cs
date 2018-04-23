using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Wappa.Challenge.Dev.Models;
using Wappa.Challenge.Dev.Negocio;

namespace Wappa.Challenge.Dev.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MotoristasController : Controller
    {
        private CadastroMotoristas _cadastroMotoristas;

        public MotoristasController(CadastroMotoristas cadastroMotoristas)
        {
            _cadastroMotoristas = cadastroMotoristas;
        }

        [HttpGet]
        public IEnumerable<Motorista> Get()
        {
            return Get(null, null);
        }

        [HttpGet("orderby/{ordenacao}/{direcao}")]
        public IEnumerable<Motorista> Get([FromRoute] string ordenacao, [FromRoute] string direcao = "ASC")
        {
            Func<Motorista, String> keySelector = null;
            switch (ordenacao)
            {
                case "nome":
                    keySelector = (m) => m.Nome;
                    break;
                case "sobrenome":
                    keySelector = (m) => m.SobreNome;
                    break;
                case "nomecompleto":
                    keySelector = (m) => m.NomeCompleto;
                    break;
            }

            return _cadastroMotoristas.ListarMotoristas(keySelector, direcao);
        }

        [HttpGet("{id}")]
        public Motorista Get(int id)
        {
            return _cadastroMotoristas.ObterMotorista(id);
        }

        [HttpPost]
        public void Post([FromBody]Motorista motorista)
        {
            motorista.ID = 0;
            if (!_cadastroMotoristas.SalvarMotorista(motorista))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        [HttpPost("{id}")]
        public void Post(int id, [FromBody]Motorista motorista)
        {
            if (motorista.ID != id)
            {
                motorista.ID = id;
            }
            if (!_cadastroMotoristas.SalvarMotorista(motorista))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (!_cadastroMotoristas.ExcluirMotorista(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
