using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wappa.Core.Utils;
using Wappa.MessageBus;

namespace Wappa.Motoristas.API.Configuration
{
	public static class MessageBusConfig
	{
		public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
		}
	}
}
