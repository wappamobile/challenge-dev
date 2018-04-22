using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wappa.API.ViewModel;
using Wappa.Business.Interfaces;
using Wappa.Models;
using Wappa.Models.Enum;

namespace Wappa.API.Controllers
{
    /// <summary>
    /// API de Motorista do Wappa
    /// </summary>
    [Route("api/[controller]")]
    public class MotoristaController : Controller
    {
        private readonly IMotoristaBusiness motoristaBusiness;
        private readonly IMapper mapper;

        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="motoristaBusiness"></param>
        /// <param name="mapper"></param>
        public MotoristaController(IMotoristaBusiness motoristaBusiness,
            IMapper mapper)
        {
            this.motoristaBusiness = motoristaBusiness;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retorna o cadastro de um motorista
        /// </summary>
        /// <param name="id">Id do motorista que deseja procurar</param>
        /// <returns>Motorista encontrado</returns>
        [HttpGet("{id:int}")]
        public IActionResult ObterPorId(int id)
        {
            var motorista = motoristaBusiness.ObterPorId(id);

            if (motorista != null)
                return Ok(mapper.Map<Motorista, MotoristaViewModel>(motorista));

            return NotFound();
        }

        /// <summary>
        /// Lista todos os motoristas cadastrado
        /// </summary>
        /// <returns>Lista de motorista</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var motoristas = motoristaBusiness.Listar(CampoOrdenacaoEnum.Nenhum);

                return Ok(mapper.Map<IEnumerable<Motorista>, IEnumerable<MotoristaViewModel>>(motoristas));
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao listar motoristas");
            }
        }

        /// <summary>
        /// Retorna lista de motorista ordenada pelo nome
        /// </summary>
        /// <returns>Lista ordenada de motoristas</returns>
        [HttpGet("[action]")]
        public IActionResult ListarOrdenadoPorNome()
        {
            try
            {
                var motoristas = motoristaBusiness.Listar(CampoOrdenacaoEnum.Nome);

                return Ok(mapper.Map<IEnumerable<Motorista>, IEnumerable<MotoristaViewModel>>(motoristas));
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao listar motoristas");
            }
        }

        /// <summary>
        /// Retorna lista de motorista ordenada pelo último nome
        /// </summary>
        /// <returns>Lista ordenada de motoristas</returns>
        [HttpGet("[action]")]
        public IActionResult ListarOrdenadoPorUltimoNome()
        {
            try
            {
                var motoristas = motoristaBusiness.Listar(CampoOrdenacaoEnum.UltimoNome);

                return Ok(mapper.Map<IEnumerable<Motorista>, IEnumerable<MotoristaViewModel>>(motoristas));
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao listar motoristas");
            }
        }

        /// <summary>
        /// Cadastra um novo motorista
        /// </summary>
        /// <param name="motorista">Motorista que será cadastrado</param>
        /// <returns>Motorista cadastrado</returns>
        [HttpPost]
        public async Task<IActionResult> Incluir([FromBody]MotoristaViewModel motorista)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var novoMotorista = mapper.Map<MotoristaViewModel, Motorista>(motorista);

                    await motoristaBusiness.Incluir(novoMotorista);

                    var motoristaVm = mapper.Map<Motorista, MotoristaViewModel>(novoMotorista);

                    return Created($"/api/motorista/{novoMotorista.Id}", motoristaVm);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao salvar o motorista");
            }
        }

        /// <summary>
        /// Atualiza o cadastro de um motorista
        /// </summary>
        /// <param name="motorista">Motorista a ser atualizado</param>
        /// <returns>Indica se o motorista foi encontrado na base para ser atualizado</returns>
        [HttpPut]
        public async Task<IActionResult> Alterar([FromBody]MotoristaViewModel motorista)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var motoristaAtualizacao = mapper.Map<MotoristaViewModel, Motorista>(motorista);

                    var motoristaAlterado = await motoristaBusiness.Alterar(motoristaAtualizacao);
                    if (motoristaAlterado)
                    {
                        var motoristaVm = mapper.Map<Motorista, MotoristaViewModel>(motoristaAtualizacao);

                        return Ok(motoristaVm);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao salvar o motorista");
            }
        }

        /// <summary>
        /// Exclui o cadastro de um motorista
        /// </summary>
        /// <param name="id">Id do motorista a ser excluído</param>
        /// <returns>Indica se o motorista foi encontrado para ser excluído</returns>
        [HttpDelete("{id:int}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var motoristaExcluído = motoristaBusiness.Excluir(id);
                if (motoristaExcluído)
                    return Ok();
                else
                    return NotFound();
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao listar motoristas");
            }
        }
    }
}