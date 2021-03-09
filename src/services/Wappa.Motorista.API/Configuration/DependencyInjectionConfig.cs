using Microsoft.Extensions.DependencyInjection;
using Wappa.Motoristas.API.Data;
using Wappa.Motoristas.API.Data.Repository;
using Wappa.Motoristas.API.Models;

namespace NSE.Clientes.API.Configuration
{
	public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<MotoristaContext>();
        }
    }
}