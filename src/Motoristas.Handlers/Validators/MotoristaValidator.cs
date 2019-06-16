using FluentValidation;
using Motoristas.Handlers.Models;

namespace Motoristas.Handlers.Validators
{
    public class MotoristaValidator : AbstractValidator<MotoristaModel>
    {
        public MotoristaValidator()
        {
            RuleFor(x => x.Nome)
               .NotEmpty()
               .WithMessage("Nome não definido.");

            RuleFor(x => x.UltimoNome)
               .NotEmpty()
               .WithMessage("Nome não definido.");

            RuleFor(x => x.Endereco)
                .NotNull()
                .WithMessage("Endereco não preenchido.");

            RuleFor(x => x.Endereco)
                .SetValidator(new EnderecoValidator());
        }
    }
}
