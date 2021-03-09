using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wappa.Core.Mediator;
using Wappa.Motoristas.API.Application.Commands;
using Wappa.Motoristas.API.Data;
using Wappa.Motoristas.API.Data.Repository;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Configuration
{
	public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IRequestHandler<AdicionarMotoristaCommand, ValidationResult>, MotoristaCommandHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Data
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<MotoristaContext>();
        }
    }
}