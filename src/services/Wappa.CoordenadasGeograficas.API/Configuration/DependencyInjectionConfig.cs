using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;
using Wappa.CoordenadasGeograficas.API.Services;

namespace Wappa.CoordenadasGeograficas.API.Configuration
{
	public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IGoogleGeocondingService, GoogleGeocondingService>()
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }
    }

	public class PollyExtensions
	{
		public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
		{
			var retry = HttpPolicyExtensions
				.HandleTransientHttpError()
				.WaitAndRetryAsync(new[]
				{
					TimeSpan.FromSeconds(1),
					TimeSpan.FromSeconds(5),
					TimeSpan.FromSeconds(10),
				}, (outcome, timespan, retryCount, context) =>
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine($"Tentando pela {retryCount} vez!");
					Console.ForegroundColor = ConsoleColor.White;
				});

			return retry;
		}
	}
}