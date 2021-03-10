using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Core.Messages.Integration;
using Wappa.MessageBus;

namespace Wappa.CoordenadasGeograficas.API.Services
{
	public class CoordenadasIntegrationHandler : BackgroundService
	{
		private readonly IMessageBus _bus;

		public CoordenadasIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
		{
			_bus = bus;			
		}

		private void SetResponder()
		{
			_bus.RespondAsync<SolicitouCadastroMotoristaIntegrationEvent, ResponseMessage>(async request =>
				await ConsultarCoordenada(request));
		}

		private Task<ResponseMessage> ConsultarCoordenada(SolicitouCadastroMotoristaIntegrationEvent request)
		{
			return Task.FromResult(new ResponseMessage(null));
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			SetResponder();
			return Task.CompletedTask;
		}
	}
}
