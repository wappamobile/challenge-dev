using Microsoft.Extensions.DependencyInjection;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.Repositorio.Repositorios;

namespace WappaChallenge.IoCContainer
{
    public class Container
    {
        public static void RegisterServices(IServiceCollection services)
        { 
            //services.AddScoped(typeof(IDatabase<,>), typeof(ArquivoTextoDB<,>));

            services.AddScoped<ICoordenadaGeograficaRepositorio, CoordenadaGeograficaRepositorio>();
            services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddScoped<IMotoristaRepositorio, MotoristaRepositorio>();
            services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
        }
    }
}
