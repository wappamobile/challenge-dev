using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

		public MotoristaController(IMediatorHandler mediator)
		{
			_mediator = mediator;
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
	}
}
