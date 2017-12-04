using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.WebApi.ViewModels.Request;

namespace Wappa.WebApi.ViewModels.Validator
{
    public class PostMotoristasRequestValidator : AbstractValidator<PostMotoristasRequest>
    {
        public PostMotoristasRequestValidator()
        {
            this.RuleFor(x => x.Nome)
              .Cascade(CascadeMode.StopOnFirstFailure)
              .NotEmpty()
              .WithMessage("required")
              .OverridePropertyName("nome");

            this.RuleFor(x => x.Sobrenome)
              .Cascade(CascadeMode.StopOnFirstFailure)
              .NotEmpty()
              .WithMessage("required")
              .OverridePropertyName("sobrenome");

            this.RuleFor(x => x.CarroId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage("required")
            .OverridePropertyName("carroId");

            this.RuleFor(x => x.Logradouro)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage("required")
            .OverridePropertyName("logradouro");

            this.RuleFor(x => x.Numero)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage("required")
            .OverridePropertyName("numero");

            this.RuleFor(x => x.Bairro)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage("required")
            .OverridePropertyName("bairro");

            this.RuleFor(x => x.Cep)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage("required")
            .OverridePropertyName("cep");

            this.RuleFor(x => x.CidadeId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .WithMessage("required")
            .OverridePropertyName("cidadeId");
        }
    }
}