using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TesteDev.Infra;
using TesteDev.Servicos;
using TesteDev.Servicos.ModelsConfiguracao;

namespace TesteDev
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var conexao = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<Contexto>(options => options
                .UseSqlServer(conexao));

            services.Configure<ConfiguracaoCoordenadas>(Configuration.GetSection("ConfiguracaoCoordenadas"));

            //Registra o Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Teste Dev", Version = "v1" });
            });

            services.AddMvc();

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            //Configuração do Injetor de Dependencia (Autofac)
            //Para registrar os Repositórios 
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ModuleInfra>();
            containerBuilder.RegisterModule<ModuleServicos>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Contexto contexto)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DbInitializer.Initialize(contexto);

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TESTEDEV V1");
            });
        }
    }
}
