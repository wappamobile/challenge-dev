using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Core.Messages;
using Wappa.Motoristas.API.Application.Extensions;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Application.Commands
{
	public class MotoristaCommandHandler : CommandHandler,
		IRequestHandler<AdicionarMotoristaCommand, ValidationResult>,
		IRequestHandler<AtualizarMotoristaCommand, ValidationResult>
	{
		private readonly IMotoristaRepository _motoristaRepository;

		public MotoristaCommandHandler(IMotoristaRepository motoristaRepository)
		{
			_motoristaRepository = motoristaRepository;
		}

		public async Task<ValidationResult> Handle(AdicionarMotoristaCommand message, CancellationToken cancellationToken)
		{
			if (!message.EhValido()) return message.ValidationResult;

			var motorista = message.MapearMotorista();

			_motoristaRepository.Adicionar(motorista);

			return await PersistirDados(_motoristaRepository.UnitOfWork);
		}

		public async Task<ValidationResult> Handle(AtualizarMotoristaCommand message, CancellationToken cancellationToken)
		{
			if (!message.EhValido()) return message.ValidationResult;
			
			var motorista = await _motoristaRepository.ObterPorId(message.Id);

			if (motorista == null)
			{
				AdicionarErro("Motorista não foi encontrado");
				return ValidationResult;
			}

			message.MapearMotorista(motorista);

			_motoristaRepository.Atualizar(motorista);

			return await PersistirDados(_motoristaRepository.UnitOfWork);
		}

		
	}
}
