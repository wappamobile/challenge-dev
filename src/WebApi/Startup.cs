using FluentValidation;
using Infra.Data;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using WebApi.Validators;
using WebApi.ViewModels.Request;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            this.Configuration = configuration;
            this.CurrentEnvironment = currentEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (CurrentEnvironment.IsEnvironment("Testing"))
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                services.AddDbContext<Context>(options =>
                    options.UseSqlite(connection));
            }
            else
            {
                services.AddDbContext<Context>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            }

            services.AddMvc();
            RegisterServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Wappa - API",
                        Version = "v1",
                        Description = "API de cadastro de motoristas",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "Heverson Ribeiro",
                            Url = "https://github.com/heversonr"
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                if (File.Exists(caminhoXmlDoc))
                {
                    c.IncludeXmlComments(Path.Combine(caminhoXmlDoc));
                }

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Wappa - API");
                c.RoutePrefix = string.Empty;

            });

            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);

            //FluentValidation
            services.AddTransient<IValidator<MotoristaCadastroPostRequest>, MotoristaCadastroPostValidator>();
            services.AddTransient<IValidator<MotoristaCadastroPutRequest>, MotoristaCadastroPutValidator>();
        }

    }
}
