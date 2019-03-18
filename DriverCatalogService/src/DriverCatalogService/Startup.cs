using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using DriverCatalogService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace DriverCatalogService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureRepository(services);
        }

        protected virtual void ConfigureRepository(IServiceCollection services)
        {
            // Adds the DynamoDB repo proxy class to the ASP.NET Core dependency injection framework.
            services.Add(new ServiceDescriptor(typeof(IRepository), typeof(DynamoDBRepository), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(IGeoLocator), typeof(GoogleGeoLocator), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(DriverValidator), typeof(DriverValidator), ServiceLifetime.Singleton));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
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
        }
    }
}
