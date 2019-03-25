using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AutoMapper;
using FluentValidation;
using WappaMobile.Application;

namespace WappaMobile.Application
{
    public static class ApplicationDependencyInjectionExtensions
    {
        public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IValidator<ModifyDriverDto>, ModifyDriverDtoValidator>();

            return services;
        }
    }
}
