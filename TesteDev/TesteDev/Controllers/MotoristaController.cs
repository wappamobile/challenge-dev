using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using TesteDev.Infra.Entidades;
using TesteDev.Infra.Repositorios.Interfaces;

namespace TesteDev.Controllers
{
    [Route("api/Motorista")]
    public class MotoristaController : Controller
    {
        private readonly IMotoristaRepositorio _motoristaRepositorio;

        public MotoristaController(IMotoristaRepositorio motoristaRepositorio)
        {
            _motoristaRepositorio = motoristaRepositorio;
        }

        /// <summary>
        /// Lista os motoristas cadastrados.
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="ultimoNome">Último nome</param>
        /// <returns>Lista com os motoristas encontrados</returns>
        [HttpGet]
        [Route("listar")]
        [ProducesResponseType(typeof(IEnumerable<Motorista>), 200)]
        public IActionResult Listar([FromHeader] string nome, [FromHeader] string ultimoNome)
        {
            var itens = _motoristaRepositorio.Listar(nome, ultimoNome);
            return Ok(itens);
        }

        /// <summary>
        /// Cria novo motorista.
        /// </summary>
        /// <param name="motorista"></param>
        /// <returns>Erro ou, em caso de sucesso, objeto motorista criado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Motorista), 200)]
        [ProducesResponseType(typeof(ModelStateDictionary), 400)]
        public IActionResult Post([FromBody]Motorista motorista)
        {
            if (motorista == null) { return BadRequest("Objeto vazio"); }

            ModelState.Clear();

            TryValidateModel(motorista);

            if (ModelState.IsValid)
            {
                _motoristaRepositorio.Criar(motorista);

                return Ok(motorista);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza um motorista existente.
        /// </summary>
        /// <param name="id">Id do objeto a ser atualizado</param>
        /// <param name="model">Objeto com as informações atualizadas</param>
        /// <returns>Retorna erro ou, em caso de sucesso, o objeto atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Motorista), 200)]
        [ProducesResponseType(typeof(ModelStateDictionary), 400)]
        public IActionResult Put(int id, [FromBody]Motorista model)
        {
            if (model == null) { return BadRequest("Objeto vazio"); }

            if (id != model.Id) { return BadRequest(); }

            if (!_motoristaRepositorio.VerificarExistencia(id)) { return StatusCode(420, "Motorista não encontrado."); }

            ModelState.Clear();

            TryValidateModel(model);

            if (ModelState.IsValid)
            {
                _motoristaRepositorio.Atualizar(model);

                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Remove motorista cadastrado.
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <returns>Erro ou, em caso de sucesso, Ok</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_motoristaRepositorio.VerificarExistencia(id))
            {
                return StatusCode(420, "Motorista não encontrado.");
            }
            _motoristaRepositorio.Remover(id);

            return StatusCode(200, "Ok");
        }
    }
}
