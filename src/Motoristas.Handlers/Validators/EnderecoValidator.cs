using FluentValidation;
using Motoristas.Handlers.Models;

namespace Motoristas.Handlers.Validators
{
    public class EnderecoValidator : AbstractValidator<EnderecoModel>
    {
        public EnderecoValidator()
        {
            RuleFor(x => x.Cep)
                .NotEmpty()
                .WithMessage("CEP não definido.");

            RuleFor(x => x.Cep)
                .SetValidator(new CEPValidator());
        }
    }
}
