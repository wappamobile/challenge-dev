using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Wappa.Business.Implementations;
using Wappa.Business.Interfaces;
using Wappa.DataAccess;
using Wappa.DataAccess.Implementations;
using Wappa.DataAccess.Interfaces;

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
            services.AddDbContext<WappaContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("WappaConnectionString"),
                    b => b.MigrationsAssembly("Wappa.API"));
            });

            services.AddAutoMapper();

            #region Business
            services.AddScoped<IMotoristaBusiness, MotoristaBusiness>();
            services.AddScoped<ILocalizacaoBusiness, LocalizacaoBusiness>();
            #endregion

            #region Data Access
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<IGoogleMapsClient, GoogleMapsClient>();
            #endregion

            services.AddMvc();

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "API de motoristas do Wappa",
                        Version = "v1",
                        Description = "API de motorista do Wappa",
                        Contact = new Contact
                        {
                            Name = "Reinaldo Bispo"
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Motoristas do Wappa");
            });
        }
    }
}
