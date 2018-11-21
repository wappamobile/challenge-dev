using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Wappa.Infra.Ioc
{
    internal static class MediatRExtensions
    {
        public static IServiceCollection AddMediatr(
            this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Wappa.Application");

            services.AddMediatR(assembly);

            return services;
        }
    }
}