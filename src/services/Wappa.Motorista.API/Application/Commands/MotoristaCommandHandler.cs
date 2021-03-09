using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Core.Messages;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Application.Commands
{
	public class MotoristaCommandHandler : CommandHandler,
		IRequestHandler<AdicionarMotoristaCommand, ValidationResult>
	{
		private readonly IMotoristaRepository _motoristaRepository;

		public MotoristaCommandHandler(IMotoristaRepository motoristaRepository)
		{
			_motoristaRepository = motoristaRepository;
		}

		public async Task<ValidationResult> Handle(AdicionarMotoristaCommand message, CancellationToken cancellationToken)
		{
			if (!message.EhValido()) return message.ValidationResult;

			var motorista = MapearMotorista(message);

			_motoristaRepository.Adicionar(motorista);

			return await PersistirDados(_motoristaRepository.UnitOfWork);
		}

		private Motorista MapearMotorista(AdicionarMotoristaCommand message)
		{
			var endereco = new Endereco
			{
				Logradouro = message.Endereco.Logradouro,
				Numero = message.Endereco.Numero,
				Complemento = message.Endereco.Complemento,
				Bairro = message.Endereco.Bairro,
				Cep = message.Endereco.Cep,
				Cidade = message.Endereco.Cidade,
				Estado = message.Endereco.Estado
			};

			var carro = new Carro(message.Carro.Marca, message.Carro.Modelo, message.Carro.Placa);

			var motorista = new Motorista(message.Nome, message.SobreNome);

			motorista.Carro = carro;
			motorista.Endereco = endereco;

			return motorista;
		}
	}
}
