using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Wappa {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            // Config do CORS
            ConfigurarCors (services);

            // Configurando injetor de serviços genérico 
            services.AddServices (typeof (Startup).Assembly);

            services.AddMvc ();

            ConfigurarDocumentacao (services);
        }

        public void ConfigurarCors (IServiceCollection services) {
            services.AddCors (options => {
                options.AddPolicy ("AllowHeaders", builder => {
                    builder.AllowAnyOrigin ()
                        .AllowAnyHeader ()
                        .AllowAnyMethod ()
                        .AllowCredentials ();
                });
            });
        }

        public void ConfigurarDocumentacao (IServiceCollection services) {
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1.Motoristas",
                    new Info {
                        Title = "Motoristas API",
                            Version = "v1",
                            Description = "API Motoristas",
                            Contact = new Contact {
                                Name = "Lucas Rossini e Silva Damian",
                                    Email = "lucas_rossini@live.com"
                            }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseCors ("AllowAllHeaders");

            app.UseAuthentication ();

            app.UseMvc ();

            app.UseSwagger (c => {
                c.RouteTemplate = "doc/{documentName}/swagger.json";
            });

            app.UseSwaggerUI (c => {
                c.RoutePrefix = "doc";
                c.SwaggerEndpoint ("../doc/v1.Motoristas/swagger.json", "Motoristas v1");
            });
        }
    }
}