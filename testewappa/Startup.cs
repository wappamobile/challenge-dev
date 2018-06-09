using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using testewappa.Models;
using Microsoft.EntityFrameworkCore;
using testewappa.Repository;
using testewappa.Integration;
using Swashbuckle.AspNetCore.Swagger;

namespace testewappa
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
            services.AddDbContext<WappaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IWappaRepository, WappaRepository>();
            services.AddTransient<IGoogleAddress, GoogleAddress>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() 
                { 
                    Title = "Challenge Dev Wappa - Thiago França da Silva", 
                    Version = "v1",
                    Description = "Challenge DEV Wappa - Cadastro e Motoristas com Consulta API da Google. \n Tive problemas com meu laptop e não consegui configurar o SQL ou Mongo para persistir os dados. \nPorém toda a estrutura foi criada: \nController, \nRepositorio, \nDocumentação Swagger, \nInjeção de Dependência e \nconsumo da API do Google" 
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge Dev Wappa - Thiago França da Silva -  V1");
            });
        }
    }
}
