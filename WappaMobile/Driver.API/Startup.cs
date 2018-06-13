using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WappaMobile.Driver.API.Infrastructure;
using WappaMobile.Driver.API.Infrastructure.Filters;
using WappaMobile.Driver.Infrastructure;
using WappaMobile.Driver.Infrastructure.Repositories;

namespace WappaMobile.Driver.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc()
                .AddSwagger()
                .Configure<DriverSettings>(Configuration);

            services
                .AddMongo()
                .AddTransient<IDriverRepository, DriverRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute()
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Driver.API V1");
                });
        }
    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(options => {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "WappaMobile - Driver API",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddScoped<IMongoDatabase>(provider => {
                var settings = provider.GetService<IOptions<DriverSettings>>();

                var client = new MongoClient(settings.Value.ConnectionString);

                return client.GetDatabase(settings.Value.Database);
            });

            services.AddScoped<IDbContext, DbContext>();

            return services;
        }
    }
}
