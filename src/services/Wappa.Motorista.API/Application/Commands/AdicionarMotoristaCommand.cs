using FluentValidation;
using Wappa.Core.Messages;
using System;
using Wappa.Motoristas.API.Application.DTO;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Application.Commands
{
	public class AdicionarMotoristaCommand : Command
    {
		// Motorista
		public string Nome { get; set; }
        public string SobreNome { get; set; }

        // Carro
        public CarroDTO Carro { get; set; }

        // Endereco
        public EnderecoDTO Endereco { get; set; }
		
        public override bool EhValido()
        {
            ValidationResult = new AdicionarMotoristaValidation().Validate(this);
            return ValidationResult.IsValid;
        }        

        public class AdicionarMotoristaValidation : AbstractValidator<AdicionarMotoristaCommand>
        {
            public AdicionarMotoristaValidation()
            {
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