using FluentValidation;
using Motoristas.Handlers.Models;

namespace Motoristas.Handlers.Validators
{
    public class VeiculoValidator : AbstractValidator<VeiculoModel>
    {
        public VeiculoValidator()
        {
            RuleFor(x => x.Marca)
               .NotEmpty()
               .WithMessage("Marca não definida.")
               .Length(2, 15)
               .WithMessage("Marca com formato incorreto.");

            RuleFor(x => x.Modelo)
              .NotEmpty()
              .WithMessage("Modelo não definido.");

            RuleFor(x => x.Placa)
               .SetValidator(new PlacaValidator());
        }
    }
}
