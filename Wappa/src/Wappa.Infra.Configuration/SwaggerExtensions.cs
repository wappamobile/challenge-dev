using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;

namespace Wappa.Infra.Ioc
{
    public static class SwaggerExtensions
    {
        private const string Url = "/swagger/v1/swagger.json";

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Wappa.Driver",
                    Version = "v1",
                    Description = "Documentação do sistema",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Márcio Adão",
                        Email = "",
                        Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                SetXmlDocumentation(c);
            });

            return services;
        }

        private static void SetXmlDocumentation(SwaggerGenOptions options)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;

            var name = PlatformServices.Default.Application.ApplicationName;

            var xmlDocumentPath = Path.Combine(path, $"{name}.xml");

            if (File.Exists(xmlDocumentPath))
            {
                options.IncludeXmlComments(xmlDocumentPath);
            }
        }

        public static IApplicationBuilder UseConfigSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Url,
                    "Wappa.Driver");
            });

            return app;
        }
    }
}