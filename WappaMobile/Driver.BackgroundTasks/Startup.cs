using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WappaMobile.Driver.BackgroundTasks.Tasks;
using WappaMobile.Driver.Infrastructure;
using WappaMobile.Driver.Infrastructure.Repositories;

namespace WappaMobile.Driver.BackgroundTasks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BackgroundTaskSettings>(Configuration);

            services
                .AddMongo()
                .AddAutoMapper();

            services
                .AddSingleton<IDriverRepository, DriverRepository>()
                .AddSingleton<IHostedService, GeolocationManagerTask>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }
    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDatabase>(provider => {
                var settings = provider.GetService<IOptions<BackgroundTaskSettings>>();

                var client = new MongoClient(settings.Value.ConnectionString);

                return client.GetDatabase(settings.Value.Database);
            });

            services.AddSingleton<IDbContext, DbContext>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(_ => { });
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
