using Driver.Application.Data.Repositories;
using Driver.Application.Data.Repositories.Common;
using Driver.Application.Data.Repositories.Interfaces;
using Driver.Application.Migration;
using Driver.Application.Services;
using Driver.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Driver.Application
{
    public class ApplicationStartup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            CreateDataBaseMigration.Run();

            services.AddScoped<SqlConnectionProvider>();

            //Repositories
            services.AddScoped<IDriverRepository, DriverRepository>();

            //Services
            services.AddScoped<IGoogleApiService, GoogleApiService>();
        }
    }
}