using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teste.Implementacao;
using Teste.Implementacao.Filtro;
using Newtonsoft.Json;
using Teste.API.Contratos;
using Teste.API.Enumerador;
using System.Net;
using Teste.API.Interface;

namespace Teste.API.Controllers
{
    [Produces("application/json")]
    [Route("api/motorista")]
    public class MotoristaController : Controller
    {
        private ICadastro cadastroMotorista;
        private ITransformacao<Motorista> converte;

        public MotoristaController(ICadastro cadastroMotorista, ITransformacao<Motorista> converte)
        {
            this.cadastroMotorista = cadastroMotorista;
            this.converte = converte;
        }

        [HttpGet]
        public JsonResult Consultar()
        {
            var motoristas = cadastroMotorista.Consultar(new FiltroMotorista());

            return Json(motoristas);
            
        }

        [HttpGet("{ordenarPor}")]
        public JsonResult Consultar(OrdenacaoPor ordenarPor)
        {
            var filtro = new FiltroMotorista();
            filtro.OrdenarMotoristaPor = Enum.Parse<OrdenacaoListaMotorista>(ordenarPor.ToString());

            var motoristas = cadastroMotorista.Consultar(filtro);

            return Json(motoristas);
        }

        [HttpPost]
        public void Post([FromBody]Motorista motorista)
        {
            var entrada = this.converte.Transformar(motorista);
            cadastroMotorista.Cadastrar(entrada);

        }

        [HttpPut]
        public void Put([FromBody]Motorista motorista)
        {
            var entrada = this.converte.Transformar(motorista);
            cadastroMotorista.Editar(entrada);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                cadastroMotorista.Excluir(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}