using FluentValidation.Results;
using System.Threading.Tasks;
using Wappa.Core.Data;

namespace Wappa.Core.Messages
{
	public abstract class CommandHandler
	{
		protected ValidationResult ValidationResult;

		protected CommandHandler()
		{
			ValidationResult = new ValidationResult();
		}

		protected void AdicionarErro(string message)
		{
			ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
		}

		protected async Task<ValidationResult> PersistirDados(IUnitOfWork unitOfWork)
		{
			if (!await unitOfWork.Commit()) AdicionarErro("Houve um erro ao persistir os dados");

			return ValidationResult;
		}
	}
}
