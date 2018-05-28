using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Wappa.API.Context;
using Wappa.Core.APIHandler;
using AutoMapper;

namespace Wappa.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath)
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adiciona permissão para Cross Origin Resource Sharing (CORS)
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCORSConfig",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyHeader()
                                             .AllowAnyMethod()
                                             .AllowCredentials()
                                             .SetPreflightMaxAge(System.TimeSpan.FromMilliseconds(500));
                                  });
            });

            // Adiciona serviços do Framework.
            services.AddMvc();

            // Adiciona como singleton as configurações
            services.AddSingleton(Configuration);

            // Adiciona as interfaces customizadas
            services.Add_Wappa_Interfaces();

            // Aplica as politcas do CORS de forma global
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("DefaultCORSConfig"));
            });

            // Adiciona o Swagger para auxiliar no teste/documentação das API
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Wappa.API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath  = Path.Combine(basePath, "Wappa.API.xml");

                options.IncludeXmlComments(xmlPath);
            });

            // Adiciona o AutoMapper
            services.AddAutoMapper();

            // Adiciona o contexto do banco de dados
            services.AddDbContext<WappaDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("WappaDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Wappa.API v1");
            });

            app.UseMiddleware<ExceptionHandler>();
            app.UseCors("DefaultCORSConfig");
            app.UseMvc();
        }
    }
}
