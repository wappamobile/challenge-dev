using System;
using Microsoft.AspNetCore.Mvc;
using WappaChallenge.AppServices.Facade.Interfaces;
using WappaChallenge.DTO;

namespace WappaChallenge.WebAPI.Cadastro.Controllers
{
    /// <summary>
    /// Serviço responsável pela gestão de Motoristas do Wappa Challenge.
    /// </summary>
    [Produces("application/json")]
    [Route("api/CadastroMotorista")]
    public class CadastroMotoristaController : BaseController
    {
        private readonly IMotoristaFacade _motoristaFacade;

        /// <summary>
        /// Construtor com Injeção de dependência.
        /// </summary>
        /// <param name="motoristaFacade"></param>
        public CadastroMotoristaController(IMotoristaFacade motoristaFacade)
        {
            this._motoristaFacade = motoristaFacade;
        }

        /// <summary>
        /// Método responsável pelo cadastro de novos motoristas.
        /// </summary>
        /// <param name="motorista">Motorista</param>
        /// <returns>Dados do motorista cadastrado.</returns>
        [HttpPost]
        [Route("cadastrarMotorista")]
        public IActionResult CadastrarMotorista([FromBody] MotoristaDTO motorista)
        {
            try
            {
                motorista.ValidarEntidade();
                return base.ResponseSucess(this._motoristaFacade.CadastrarMotorista(motorista));
            }
            catch (Exception e)
            {
                return base.ReponseException(e);
            }

            
        }

        /// <summary>
        /// Método responsável por devolver uma lista de Motoristas (Ordem ascendente pelo primeiro nome).
        /// </summary>
        /// <returns>Lista ordernada de Motoristas.</returns>
        [HttpGet]
        [Route("obterTodosMotoristasOrdenadoPorPrimeiroNome")]
        public IActionResult ObterTodosMotoristasOrdenadoPorPrimeiroNome()
        {
            try
            {
                return base.ResponseSucess(this._motoristaFacade.ObterTodosOrdenadoPorPrimeiroNome());
            }
            catch (Exception e)
            {
                return base.ReponseException(e);
            }
        }

        /// <summary>
        /// Método responsável por devolver uma lista de Motoristas (Ordem ascendente pelo ultimo nome).
        /// </summary>
        /// <returns>Lista ordernada de Motoristas.</returns>
        [HttpGet]
        [Route("obterTodosMotoristasOrdenadoPorUltimoNome")]
        public IActionResult ObterTodosMotoristasOrdenadoPorUltimoNome()
        {
            try
            {
                return base.ResponseSucess(this._motoristaFacade.ObterTodosOrdenadoPorUltimoNome());
            }
            catch (Exception e)
            {
                return base.ReponseException(e);
            }
        }

        /// <summary>
        /// Método responsável por atualizar os dados cadastrais dos motoristas.
        /// </summary>
        /// <param name="motorista">Motorista</param>
        /// <returns>Motorista com os dados atualizados.</returns>
        [HttpPut]
        [Route("atualizarMotorista")]
        public IActionResult atualizarMotorista([FromBody] MotoristaDTO motorista)
        {
            try
            {
                motorista.ValidarEntidade();
                return base.ResponseSucess(this._motoristaFacade.AtualizarMotorista(motorista));
            }
            catch (Exception e)
            {
                return base.ReponseException(e);
            }
        }

        /// <summary>
        /// Método responsável pela exclusão de motoristas.
        /// </summary>
        /// <param name="id">ID de cadastro do motorista.</param>
        /// <returns>Mensagem padrão de retorno da API.</returns>
        [HttpDelete]
        [Route("excluirMotorista")]
        public IActionResult excluirMotorista([FromBody] int id)
        {
            try
            {
                this._motoristaFacade.ExcluirMotorista(id);
                return base.ResponseSucess();
            }
            catch (Exception e)
            {
                return base.ReponseException(e);
            }
        }
    }
}