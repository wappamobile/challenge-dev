using Microsoft.Extensions.DependencyInjection;
using WappaChallenge.AppServices.Facade;
using WappaChallenge.AppServices.Facade.Interfaces;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.Repositorio.Databases;
using WappaChallenge.Repositorio.Repositorios;

namespace WappaChallenge.IoCContainer
{
    public class Container
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IDatabase), typeof(MockStaticDatabase));

            services.AddScoped<IMotoristaFacade, MotoristaFacade>();

            services.AddScoped<ICoordenadaGeograficaRepositorio, CoordenadaGeograficaRepositorio>();
            services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddScoped<IMotoristaRepositorio, MotoristaRepositorio>();
            services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
        }
    }
}
