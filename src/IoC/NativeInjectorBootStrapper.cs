using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Flurl.Http.Configuration;
using Infra.Data;
using Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application Services
            services.AddScoped<IMotoristaService, MotoristaService>();
            services.AddScoped<ICarroService, CarroService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IGeocodeService, GeocodeService>();

            //Flurl
            services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

            // Infra
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<ICarroRepository, CarroRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IGeoLocationRepository, GeoLocationRepository>();
            services.AddScoped<Context>();

        }

    }
}
