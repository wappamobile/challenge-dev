using Wappa.API.Context;
using Wappa.Core.Repositories;
using Wappa.Core.Services;
using Wappa.Core.UnitOfWork;
using Wappa.Entities;
using Wappa.Services;
using Wappa.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection Add_Wappa_Interfaces(this IServiceCollection services)
        {
            // Adiciona as interfaces "base" para o projeto
            services.AddScoped<IUnitOfWork, UnitOfWork<WappaDbContext>>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<,>));
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            // Cria de forma genérica um repositório que implementa a classe "RepositoryBase"
            services.AddScoped<IRepositoryBase<Carro>>(p => new RepositoryBase<Carro, WappaDbContext>(p.GetService<WappaDbContext>()));
            services.AddScoped<IRepositoryBase<Endereco>>(p => new RepositoryBase<Endereco, WappaDbContext>(p.GetService<WappaDbContext>()));
            services.AddScoped<IRepositoryBase<Motorista>>(p => new RepositoryBase<Motorista, WappaDbContext>(p.GetService<WappaDbContext>()));
            
            // Adiciona os serviços
            services.AddScoped<ICarroService, CarroService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IMotoristaService, MotoristaService>();
            
            return services;
        }
    }
}
