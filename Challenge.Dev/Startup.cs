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
using Microsoft.EntityFrameworkCore;
using Challenge.Dev.Context;
using Swashbuckle.AspNetCore.Swagger;
using Challenge.Dev.Repositories;
using Challenge.Dev.Repositories.Impl;

namespace Challenge.Dev
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChallengeDevDbContext>(opt => opt.UseInMemoryDatabase("ChallengeDev"));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMvc(options =>
           {
               options.Filters.Add(typeof(ValidateModelStateAttribute));

           });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Challenge Dev - Adilson", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge Dev Adilson");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
