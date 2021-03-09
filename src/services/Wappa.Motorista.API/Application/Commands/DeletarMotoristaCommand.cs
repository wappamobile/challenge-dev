using FluentValidation;
using Wappa.Core.Messages;
using System;

namespace Wappa.Motoristas.API.Application.Commands
{
	public class DeletarMotoristaCommand : Command
    {
		// Motorista
		public Guid Id { get; set; }
        public override bool EhValido()
        {
            ValidationResult = new DeletarMotoristaValidation().Validate(this);
            return ValidationResult.IsValid;
        }        

        public class DeletarMotoristaValidation : AbstractValidator<DeletarMotoristaCommand>
        {
            public DeletarMotoristaValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do motorista inválido");
            }
        }
    }
}