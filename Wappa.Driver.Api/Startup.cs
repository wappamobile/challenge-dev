using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Wappa.Driver.Api.Data.Context;
using Wappa.Driver.Api.Dtos;
using Wappa.Driver.Api.Repositories.Implementations;
using Wappa.Driver.Api.Repositories.Interfaces;
using Wappa.Driver.Api.Services.Implementations;
using Wappa.Driver.Api.Services.Interfaces;

namespace Wappa.Driver.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Driver API", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Wappa.Driver.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            var appSettings = new AppSettings(Configuration["connectionstring"], Configuration["wappa.driver.map.key"]);
            services.AddScoped(serv => appSettings);
            services.AddScoped<IMapsService, MapsService>();
            services.AddScoped<IDriverRepository<DriverDto>, DriverRepository>();
            services.AddDbContext<DriverContext>(o => o.UseSqlServer(appSettings.ConnectionString));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Driver");
            });

            app.UseMvc();
        }
    }
}
