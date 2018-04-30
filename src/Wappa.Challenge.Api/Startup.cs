using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Wappa.Challenge.Api.Filters;
using Wappa.Challenge.Domain.Handlers;
using Wappa.Challenge.Domain.Repositories;
using Wappa.Challenge.Domain.Services;
using Wappa.Challenge.Infrastructure.Repositories;
using Wappa.Challenge.Services;
using Wappa.Challenge.Shared.Configuration;

namespace Wappa.Challenge.Api
{
    /// Startup class
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// Constructor
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.Filters.Add(new ModelStateValidationFilter());
            });
            services.AddAutoMapper();

            ConfigureDependencyInjection(services);

            ConfigureSwagger(services);
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Driver API");
            });
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            var mongoClient = new MongoClient(_configuration.GetConnectionString("Wappa"));
            var db = mongoClient.GetDatabase("Wappa");

            services.AddSingleton(w => db);

            services.AddTransient<DriverHandler, DriverHandler>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<IGoogleService, GoogleService>();

            services.AddSingleton(_configuration.GetSection("GoogleMapsConfiguration").Get<GoogleMapsConfiguration>());
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Wappa Challenge Dev",
                    Version = "v1",
                    Description = "Driver API",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Felipe Jesus",
                        Email = "felipepiresdejesus@gmail.com",
                        Url = "https://github.com/felipepiresdejesus"
                    }
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Wappa.Challenge.Api.xml");
                s.IncludeXmlComments(xmlPath);
            });
        }
    }
}
