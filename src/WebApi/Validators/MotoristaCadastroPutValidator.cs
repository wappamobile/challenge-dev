using FluentValidation;
using WebApi.ViewModels.Request;

namespace WebApi.Validators
{
    public class MotoristaCadastroPutValidator : AbstractValidator<MotoristaCadastroPutRequest>
    {
        public MotoristaCadastroPutValidator()
        {
            this.RuleFor(x => x.MotoristaId)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.Sobrenome)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.MarcaCarro)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.ModeloCarro)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.PlacaCarro)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.CEP)
                .NotEmpty()
                .WithMessage("required")
                .Must(HelperValidator.ValidNumber)
                .WithMessage("invalid");

            this.RuleFor(x => x.Cidade)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.Logradouro)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.Complemento)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage("required");

            this.RuleFor(x => x.UF)
                .NotEmpty()
                .WithMessage("required");

            this.When(x => !string.IsNullOrEmpty(x.UF),
                () =>
                {
                    RuleFor(x => x.UF).Length(2);
                });



        }
    }
}
