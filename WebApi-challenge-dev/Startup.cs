using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi_challengedev.Data;

namespace WebApi_challenge_dev
{
    public class Startup
    
    {
        public IConfiguration Configuration { get; }
        

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        
        //configurações do serviço
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MotoristasContext>();
            services.AddMvc();
        }

        // Metodo chamado na execução.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("API WAPPA CRUD: MOTORISTAS");
            });
        }
    }
}
