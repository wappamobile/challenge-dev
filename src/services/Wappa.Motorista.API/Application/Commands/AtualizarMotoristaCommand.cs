using FluentValidation;
using Wappa.Core.Messages;
using System;
using Wappa.Motoristas.API.Application.DTO;

namespace Wappa.Motoristas.API.Application.Commands
{
	public class AtualizarMotoristaCommand : Command
    {
        public Guid Id { get; set; }
		// Motorista
		public string Nome { get; set; }
        public string SobreNome { get; set; }

        // Carro
        public CarroDTO Carro { get; set; }

        // Endereco
        public EnderecoDTO Endereco { get; set; }
		
        public override bool EhValido()
        {
            ValidationResult = new AtualizarMotoristaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarMotoristaValidation : AbstractValidator<AtualizarMotoristaCommand>
        {
            public AtualizarMotoristaValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do motorista inválido");

                RuleFor(c => c.Nome)
                    .NotNull()
                    .WithMessage("Inform o nome do motorista");

                RuleFor(c => c.SobreNome)
                    .NotNull()
                    .WithMessage("Inform o sobrenome do motorista");

                RuleFor(c => c.Carro)
                    .NotNull()
                    .WithMessage("Informe o carro do motorista");

                RuleFor(c => c.Endereco)
                    .NotNull()
                    .WithMessage("Informe o endereço do motorista");
            }
        }
    }
}