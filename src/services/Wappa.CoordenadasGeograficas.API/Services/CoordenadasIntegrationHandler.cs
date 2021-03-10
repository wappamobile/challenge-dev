using FluentValidation.Results;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wappa.CoordenadasGeograficas.API.Models;
using Wappa.Core.Messages.Integration;
using Wappa.MessageBus;

namespace Wappa.CoordenadasGeograficas.API.Services
{
	public class CoordenadasIntegrationHandler : BackgroundService
	{
		private readonly IMessageBus _bus;
		private readonly IGoogleGeocondingService _googleGeocondingService;

		public CoordenadasIntegrationHandler(IMessageBus bus, IGoogleGeocondingService googleGeocondingService)
		{
			_bus = bus;
			_googleGeocondingService = googleGeocondingService;
		}

		private void SetResponder()
		{
			_bus.RespondAsync<SolicitouCadastroMotoristaIntegrationEvent, ResponseMessage>(async request =>
				await ConsultarCoordenada(request));
		}

		private async Task<ResponseMessage> ConsultarCoordenada(SolicitouCadastroMotoristaIntegrationEvent request)
		{
			var enderecoConsultar = new Endereco(request.Logradouro,
				request.Numero, 
				request.Complemento, 
				request.Bairro, 
				request.Cep, 
				request.Cidade, 
				request.Estado);

			var result = await _googleGeocondingService.BuscarCoordenadas(enderecoConsultar);

			var coordenadaResponse = new ResponseMessage(new ValidationResult(), result.Longitude, result.Latitude);		

			return coordenadaResponse;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			SetResponder();
			return Task.CompletedTask;
		}
	}
}
