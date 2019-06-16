using FluentValidation;
using Motoristas.Helpers;

namespace Motoristas.Handlers.Validators
{
    public class PlacaValidator : AbstractValidator<string>
    {
        public PlacaValidator()
        {
            RuleFor(x => x)
                .Must(x => PlacaHelper.Validar(x))
                .WithMessage("Placa com formato inválido.");
        }
    }
}