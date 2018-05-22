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
using Wappa.Core.Interfaces;
using Wappa.Infra.Repositories;
using Wappa.Service.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Wappa.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {            
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var googleMapsBaseUrl = this.Configuration.GetSection("GoogleMapsApi:BaseUrl").Value;
            var googleMapsKey = this.Configuration.GetSection("GoogleMapsApi:Key").Value;
            var dbConnectionString =
                Environment.CurrentDirectory + "\\" + 
                this.Configuration.GetSection("LiteDatabase:ConnectionString").Value;

            services.AddMvc();
            services.AddTransient<IMotoristaService, MotoristaService>();
            services.AddTransient<IMotoristaRepository, MotoristaRepository>(
                x => new MotoristaRepository(dbConnectionString));
            services.AddTransient<ICoordenadasRepository, GoogleMapsCoordenadasRepository>(
                x => new GoogleMapsCoordenadasRepository(googleMapsBaseUrl, googleMapsKey));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1", 
                    new Info { Title = "API de Gerenciamento de Motoristas", 
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Pedro Fogolin",
                        Email = "pedro.fogolin@gmail.com"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(LogLevel.Debug);
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Gerenciamento de Motoristas");
            });
        }
    }
}
