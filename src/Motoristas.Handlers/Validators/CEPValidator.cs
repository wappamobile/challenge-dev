using FluentValidation;
using Motoristas.Helpers;

namespace Motoristas.Handlers.Validators
{
    public class CEPValidator : AbstractValidator<string>
    {
        public CEPValidator()
        {
            RuleFor(x => x)
                .Must(x => CEPHelper.Validar(x))
                .WithMessage("Formato de CEP incorreto.");
        }
    }
}
