using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Wappa.Domain.Interfaces.Connectors;
using Wappa.Infra.CrossCutting;
using Wappa.Infra.Data.Connectors;

namespace Wappa.Infra.Ioc
{
    public static class ContainerProxy
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddMediatr()
                    .AddSwagger()
                    .AddAutoMapper()
                    .AddRepositories()
                    .AddLogging()
                    .AddTransient<IGoogleMapsConnector, GoogleMapsConnector>()
                    .AddTransient<IGoogleMapsConfiguration, GoogleMapsConfiguration>();

            return services;
        }
    }
}