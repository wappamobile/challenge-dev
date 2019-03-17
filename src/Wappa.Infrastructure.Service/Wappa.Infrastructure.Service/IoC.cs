using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Wappa.Infrastructure.Service.Implementation;
using Wappa.Infrastructure.Service.Interface;

namespace Wappa.Infrastructure.Service
{
    public static class IoC
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("GoogleAPI", cfg =>
            {
                var url = string.Format(configuration["Google:MapsApi"], configuration["Google:Key"]);
                cfg.BaseAddress = new Uri(url);
                cfg.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));                
            })
            .ConfigurePrimaryHttpMessageHandler(h => new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip,
            });


            services.AddScoped<IGoogleMapsService, GoogleMapsService>();
        }
    }
}
