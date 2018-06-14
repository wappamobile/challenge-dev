using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Teste.API
{
    public partial class Startup
    {
        private void ConfigureSwagger(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
            }

            app.UseSwagger(s => s.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value));
        }


        private void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                string basePath = PlatformServices.Default.Application.ApplicationBasePath;
                string moduleName = GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml");
                string filePath = Path.Combine(basePath, moduleName);
                string readme = File.ReadAllText(Path.Combine(basePath, "README.md"));

                ApiKeyScheme scheme = Configuration.GetSection("ApiKeyScheme").Get<ApiKeyScheme>();
                options.AddSecurityDefinition("Authentication", scheme);

                Info info = Configuration.GetSection("Info").Get<Info>();
                info.Description = readme;
                options.SwaggerDoc(info.Version, info);

                options.IncludeXmlComments(filePath);
                options.DescribeAllEnumsAsStrings();

            });
        }

    }
}
