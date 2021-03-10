using FluentValidation;
using FluentValidation.Results;

namespace Wappa.Core.Messages.Integration
{
	public class SolicitouCadastroMotoristaIntegrationEvent : IntegrationEvent
	{
		public SolicitouCadastroMotoristaIntegrationEvent(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado)
		{
			Logradouro = logradouro;
			Numero = numero;
			Complemento = complemento;
			Bairro = bairro;
			Cep = cep;
			Cidade = cidade;
			Estado = estado;
		}

		public string Logradouro { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
		public string Bairro { get; set; }
		public string Cep { get; set; }
		public string Cidade { get; set; }
		public string Estado { get; set; }

		public ValidationResult ValidationResult { get; protected set; }

		public bool EhValido()
		{
			var erros = new SolicitouCadastroMotoristaIntegrationEventValidation().Validate(this).Errors;
			
			ValidationResult = new ValidationResult(erros);

			return ValidationResult.IsValid;
		}

		public class SolicitouCadastroMotoristaIntegrationEventValidation : AbstractValidator<SolicitouCadastroMotoristaIntegrationEvent>
		{
			public SolicitouCadastroMotoristaIntegrationEventValidation()
			{
				RuleFor(c => c.Logradouro)
					.NotNull()
					.WithMessage("Informe o logradouro");

				RuleFor(c => c.Cidade)
					.NotNull()
					.WithMessage("Informe a cidade");

				RuleFor(c => c.Estado)
					.NotNull()
					.WithMessage("Informe o estado");
			}
		}
	}
}
