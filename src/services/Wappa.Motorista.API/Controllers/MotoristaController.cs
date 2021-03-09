using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSE.Pedidos.API.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Core.Mediator;
using Wappa.Motoristas.API.Application.Commands;

namespace Wappa.Motoristas.API.Controllers
{
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

		[HttpPost]
		public async Task<IActionResult> AdicionarMotorista(AdicionarMotoristaCommand motorista)
		{
			return CustomResponse(await _mediator.EnviarComando(motorista));
		}

		[HttpPut]
		public async Task<IActionResult> AtualizarMotorista(AtualizarMotoristaCommand motorista)
		{
			return CustomResponse(await _mediator.EnviarComando(motorista));
		}

		[HttpDelete]
		public async Task<IActionResult> DeletarMotorista(DeletarMotoristaCommand motorista)
		{
			return CustomResponse(await _mediator.EnviarComando(motorista));
		}

		[HttpGet]
		public async Task<IActionResult> ListarMotorista()
		{
			var motoristas = await _motoristaQueries.ObterListaMotoristas();

			return motoristas == null ? NotFound() : CustomResponse(motoristas);
		}
	}
}
