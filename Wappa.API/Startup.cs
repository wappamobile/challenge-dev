using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Wappa.Dominio.Repositorio;
using Wappa.Dominio.Servico;
using Wappa.Infraestrutura.Contexto;
using Wappa.Infraestrutura.Repositorio;
using Wappa.Infraestrutura.Servico;

namespace Wappa.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json");
            

            Configuration = builder.Build();

            services.AddMvc();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Wappa API", Version = "v1" });
            });


            services.AddResponseCompression();
            services.AddScoped<WappaContexto, WappaContexto>();
            services.AddTransient<IMotoristaRepositorio, MotoristaRepositorio>();
            services.AddTransient<ICarroRepositorio, CarroRepositorio>();
            services.AddTransient<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddTransient<IGoogleMap, GoogleMap>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wappa API - V1");
            });
        }
    }
}