using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using DriverLib.Domain;
using DriverLib.Domain.Validators;
using DriverLib.Infra.CrossCutting.Identity;
using DriverLib.Infra.CrossCutting.Identity.Interfaces;
using DriverLib.Repository;
using DriverLib.Repository.UoW;
using DriverLib.Service;
using DriverLib.Service.Upload;
using DriverLib.Jobs;

namespace DriverLib.Api.Configuration
{
    public static class ServiceRepositoryCollectionExtensions
    {
        public static IServiceCollection RegisterRepositoryServices(
           this IServiceCollection services)
        {
            //services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserEmailService, UserEmailService>();
            

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobHistoryRepository, JobHistoryRepository>();

            //validators
            services.AddScoped<IValidator<User>, UserValidator>();

            //Auth
            services.AddScoped<IApplicationSignInManager, ApplicationSignInManager>();

            //Email 
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IEmailTemplate, EmailTemplate>();

            //Upload 
            services.AddScoped<IUploadService, UploadService>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Jobs
            services.AddScoped<IJobExecutor, JobExecutor>();

            return services;
        }
    }
}
