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
using Wappa.Infraestructure.Data;
using Wappa.Domain.Services;
using Wappa.Infraestructure.GoogleMaps;

namespace Wappa.API
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
            services.AddDbContext<DatabaseContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("Wappa")));

            services.Configure<GoogleMapsConfiguration>(Configuration.GetSection("GoogleMaps"));

            services.AddMvc();

            services.AddTransient<IMotoristaService, MotoristaService>();
            services.AddTransient<IMotoristaGateway, MotoristaGateway>();
            services.AddTransient<ILocalizacaoGateway, LocalizacaoGateway>();
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
