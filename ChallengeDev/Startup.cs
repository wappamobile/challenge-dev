using System.IO;
using Domain.Repository;
using Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace ChallengeDev
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup Class Contrstructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// IConfiguration Property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Cadastro de Motoristas",
                        Version = "v1",
                        Description = "API para gerenciamento do cadastro de motoristas em ASP.NET Core",
                        Contact = new Contact
                        {
                            Name = "Danilo Oliveira",
                            Url = "https://github.com/ds-oliveira"
                        }
                    });

                var applicationPath =
                    PlatformServices.Default.Application.ApplicationBasePath;
                var applicationName =
                    PlatformServices.Default.Application.ApplicationName;
                var xmlPath =
                    Path.Combine(applicationPath, $"{applicationName}.xml");

                c.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<ChallengeDevEntityContext>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IGeocodingRepository, GeocodingRepository>();
            services.AddScoped<IGeocodingService, GeocodingService>();
            services.AddHttpClient();
        }

        /// <summary>
        /// Configure the specified app, env and loggerFactory.
        /// </summary>
        /// <param name="app">App.</param>
        /// <param name="env">Env.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Cadastro de Motoristas");
            });
        }
    }
}
