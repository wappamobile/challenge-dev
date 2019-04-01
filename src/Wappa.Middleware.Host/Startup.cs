using Autofac;
using Autofac.Extensions.DependencyInjection;
using Wappa.Middleware.Autofac;
using Wappa.Middleware.Core.Extensions;
using Wappa.Middleware.Domain.Configuration;
using Wappa.Middleware.Extensions;
using Wappa.Middleware.Host.Controllers;
using Wappa.Middleware.Host.Controllers.Cars;
using Wappa.Middleware.Host.Controllers.Drivers;
using Wappa.Middleware.Host.Middleware;
using Wappa.Middleware.Miscellaneous;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Wappa.Middleware.Host
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            _configuration = AppConfig.Get(Directory.GetCurrentDirectory(), env.EnvironmentName);

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddCors(
                options => options.AddPolicy(_configuration.GetDefaultPolicy(),
                builder => builder
                    .WithOrigins(_configuration.GetCorsOrigins())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                ));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Wappa Middleware",
                        Version = "v1",
                        Description = "Wappa Middleware API",
                        Contact = new Contact
                        {
                            Name = "Wappa",
                            Url = ""
                        }
                    });
            });

            var googleOptions = new GoogleMapsConfiguration();
            new ConfigureFromConfigurationOptions<GoogleMapsConfiguration>(
                Configuration.GetSection("GoogleMapsConfigurations"))
                    .Configure(googleOptions);

            services.AddGoogleMaps(ops =>
            {
                ops.BaseUrl = googleOptions.BaseUrl;
                ops.ApiUrlAddress = googleOptions.ApiUrlAddress;
                ops.Key = googleOptions.Key;
            });

            services.AddEntityFrameworkSqlServer();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            
            containerBuilder.RegisterType<HomeController>();
            containerBuilder.RegisterType<DriverController>();
            containerBuilder.RegisterType<CarController>();

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wappa Middleware API v1.1");
            });

           

            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseCors(_configuration.GetDefaultPolicy());

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
