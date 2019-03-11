using Driver.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Driver.Api
{
    /// <summary>
    /// Startup de configurações da api
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Nova instancia de startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configurações
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configura o serviço
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddAuthorization();

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new Info
                {
                    Title = "Drivers",
                    Version ="v1",
                    Description = "Api de demonstração de manutenção de motoristas"
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

                if (File.Exists(xmlPath))
                    a.IncludeXmlComments(xmlPath);
            });

            services.ConfigureSwaggerGen(config =>
            {
                config.DescribeStringEnumsInCamelCase();

                config.DescribeStringEnumsInCamelCase();
                config.DescribeAllEnumsAsStrings();
                config.DescribeStringEnumsInCamelCase();
            });

            ApplicationStartup.ConfigureServices(services, Configuration);
        }

        /// <summary>
        /// Utiliza as configurações de serviço para fazer configurações concretas
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAllOrigins");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Driver");
            });
        }
    }
}