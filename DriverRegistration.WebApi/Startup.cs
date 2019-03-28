using DriverRegistration.Application;
using DriverRegistration.Data.MongoDb.Repositories;
using DriverRegistration.Domain.Repositories.Interfaces;
using DriverRegistration.Domain.Services;
using DriverRegistration.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DriverRegistration.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Driver & Maps Endpoints", Version = "v1" });
            });
            
            services.AddScoped(typeof(IDriverRepository), typeof(DriverRepository));
            services.AddScoped(typeof(IDriverService), typeof(DriverService));
            services.AddScoped(typeof(IMapsService), typeof(MapsService));
            services.AddScoped(typeof(DriverApplication));
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

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Driver & Maps Endpoints");
                c.RoutePrefix = "";
            });
        }
    }
}
