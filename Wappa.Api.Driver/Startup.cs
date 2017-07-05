using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wappa.Service.Geocoder;
using Swashbuckle.AspNetCore.Swagger;
using Wappa.Framework.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using System.Web.Http;

namespace Wappa.Framework.Driver
{
    /// <summary>
    /// Classe de Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddOptions();

            services.AddSingleton<IGeocodingService>(new GeocodingService(Configuration.GetValue<string>("googleApiKey")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Wappa Driver Api",
                    Version = "v1",
                    Description = "Wappa Driver Web API Documentation",
                    Contact = new Contact { Name = "Jeffersonn Barboza", Email = "jeffersonnlucas@gmail.com", Url = "https://www.linkedin.com/in/jeffersonnlucas" }
                });
                c.IncludeXmlComments(string.Format(@"{0}\.xml", AppContext.BaseDirectory));
                c.DescribeAllEnumsAsStrings();
            });

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DriverContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BaseLiveDemo")));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app</param>
        /// <param name="env">env</param>
        /// <param name="loggerFactory">loogFactory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wappa Driver Api V1");
            });
        }
    }
}
