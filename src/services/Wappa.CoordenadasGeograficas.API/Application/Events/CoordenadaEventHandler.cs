using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Wappa.CoordenadasGeograficas.API.Application.Events
{
	public class CoordenadaEventHandler : INotificationHandler<SolicitouCadastroMotoristaEvent>
	{
		public Task Handle(SolicitouCadastroMotoristaEvent notification, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
