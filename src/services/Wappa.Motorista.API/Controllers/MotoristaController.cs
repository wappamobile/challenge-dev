using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wappa.Core.Mediator;
using Wappa.Motoristas.API.Application.Commands;
using System.Net;
using FluentValidation.Results;
using Swashbuckle.AspNetCore.Annotations;
using Wappa.Motoristas.API.Application.Queries;
using Wappa.WebAPI.Core.Controllers;

namespace Wappa.Motoristas.API.Controllers
{
	/// <summary>
	/// cadastro de motoristas
	/// </summary>
	[Route("api/motorista")]
	public class MotoristaController : MainController
	{
		private readonly IMediatorHandler _mediator;
		private readonly IMotoristaQueries _motoristaQueries;

		public MotoristaController(IMediatorHandler mediator,
			IMotoristaQueries motoristaQueries)
		{
			_mediator = mediator;
			_motoristaQueries = motoristaQueries;
		}

		/// <summary>
		/// Adiciona um novo motorista ao sistema
		/// </summary>
		/// <param name="motorista">Dados do motoristas</param>
		/// <returns>Retorna um 200 ou 400</returns>
		[HttpPost]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Retorna quando sucesso.")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Retorna quando ocorre alguma falha.")]
		public async Task<IActionResult> AdicionarMotorista(AdicionarMotoristaCommand motorista)
		{
			return CustomResponse(await _mediator.EnviarComando(motorista));
		}

		/// <summary>
		/// Atualiza os dados de um motorista cadastrado no sistema
		/// </summary>
		/// <param name="motorista">Dados do motoristas</param>
		/// <returns>Retorna um 200 ou 400</returns>
		[HttpPut]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Retorna quando sucesso.")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Retorna quando ocorre alguma falha.")]
		public async Task<IActionResult> AtualizarMotorista(AtualizarMotoristaCommand motorista)
		{
			return CustomResponse(await _mediator.EnviarComando(motorista));
		}

		/// <summary>
		/// Deleta o cadastro do motorista do sistema
		/// </summary>
		/// <param name="motorista">Dados do motoristas</param>
		/// <returns>Retorna um 200 ou 400</returns>
		[HttpDelete]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Retorna quando sucesso.", Type = typeof(ValidationResult))]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Retorna quando ocorre alguma falha.", Type = typeof(ValidationResult))]
		public async Task<IActionResult> DeletarMotorista(DeletarMotoristaCommand motorista)
		{
			return CustomResponse(await _mediator.EnviarComando(motorista));
		}

		/// <summary>
		/// Busca todos os motoristas cadastrados no sistema
		/// </summary>
		/// <returns>Retorna um 200 ou 400 ou 404</returns>
		[HttpGet]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Retorna quando sucesso.")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Retorna quando ocorre alguma falha.")]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Retorna quando ocorre alguma falha.")]
		public async Task<IActionResult> ListarMotorista()
		{
			var motoristas = await _motoristaQueries.ObterListaMotoristas();

			return motoristas == null ? NotFound() : CustomResponse(motoristas);
		}
	}
}
