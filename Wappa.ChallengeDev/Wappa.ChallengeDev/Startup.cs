using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Wappa.Application;
using Wappa.DataAccess;
using Wappa.DataAccess.Contracts;

namespace Wappa.ChallengeDev
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WappaDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:EF"],
                b => b.MigrationsAssembly("Wappa.DataAccess")));
            services.AddTransient<ITaxistaRepository, EFTaxistaRepository>();
            services.AddTransient<IGeocoding, GeocodingProxy>();
            services.AddTransient<ITaxistaApp, TaxistaApp>();
            services.AddTransient<IGeocodingApp, GeocodingApp>();
            services.AddTransient<ITaxistaFacade, TaxistaFacade>();
            services.AddMvc().AddJsonOptions((options) =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WAPPA - Web APIs", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Wappa.ChallengeDev.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler("/Taxista/Error");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Taxista}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseBrowserLink();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Taxista}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            SeedData.EnsurePopulated(serviceProvider);
        }
    }
}
