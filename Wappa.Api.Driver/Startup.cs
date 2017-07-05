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

namespace Wappa.Framework.Driver
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            });

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DriverContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BaseLiveDemo")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
