using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wappa.Challenge.Dev.Data;
using Wappa.Challenge.Dev.Data.Impl;
using Wappa.Challenge.Dev.GoogleMaps.Services.Impl;
using Wappa.Challenge.Dev.GoogleMaps.Services.Impl.Configuration;
using Wappa.Challenge.Dev.Models;
using Wappa.Challenge.Dev.Negocio;
using Wappa.Challenge.Dev.Services;

namespace Wappa.Challenge.Dev.WebApi
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
            services.AddMvc();

            services.AddSingleton(Configuration.GetSection("GoogleMapsConfiguration").Get<GoogleMapsConfiguration>());
            services.AddSingleton<IGeoCoordenadaService, GoogleMapsGeoCoordenadaService>();
            services.AddScoped<IBaseRepositorio<Motorista>, MotoristaRepositorio>();
            services.AddScoped<CadastroMotoristas>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
