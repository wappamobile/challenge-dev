using FluentValidation.Results;
using Wappa.Core.Messages;
using System.Threading.Tasks;

namespace Wappa.Core.Mediator
{
	public interface IMediatorHandler
	{
		Task PublicarEvento<T>(T evento) where T : Event;
		Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
	}
}
