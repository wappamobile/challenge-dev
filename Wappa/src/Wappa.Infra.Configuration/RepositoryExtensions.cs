using Microsoft.Extensions.DependencyInjection;
using Wappa.Infra.Data.Repositories;

namespace Wappa.Infra.Ioc
{
    internal static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.Scan(config =>
            {
                config.FromAssemblyOf<Repository>()
                    .AddClasses(classes => classes.AssignableTo<Repository>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            return services;
        }
    }
}