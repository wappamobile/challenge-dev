using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Unity;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;
using Wappa.Challenge.ApplicationCore.Services;
using Wappa.Challenge.Infrastructure.Context;
using Wappa.Challenge.Infrastructure.Repositories;

namespace Wappa.Challenge.WebApi
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
            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddSingleton<IMotoristaService, MotoristaService>();
            services.AddSingleton<IMotoristaRepository, MotoristaRepository>();

            services.AddSingleton<ICarroService, CarroService>();
            services.AddSingleton<ICarroRepository, CarroRepository>();

            services.AddSingleton<IEnderecoService, EnderecoService>();
            services.AddSingleton<IEnderecoRepository, EnderecoRepository>();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });


            // 2) Generate in-memory swagger doc
            //var swaggerProvider = new SwaggerGenerator(
            //    httpConfig.Services.GetApiExplorer(),
            //    httpConfig.Formatters.JsonFormatter.SerializerSettings,
            //    new Dictionary<string, Info> { { "v1", new Info { version = "v1", title = "My API" } } },
            //    new SwaggerGeneratorOptions(
            //        // apply your swagger options here ...
            //        schemaIdSelector: (type) => type.FriendlyId(true),
            //        conflictingActionsResolver: (apiDescriptions) => apiDescriptions.First()
            //    )
            //);
            //var swaggerDoc = swaggerProvider.GetSwagger("http://tempuri.org/api", "v1");

            //// 3) Serialize
            //var swaggerString = JsonConvert.SerializeObject(
            //    swaggerDoc,
            //    Formatting.Indented,
            //    new JsonSerializerSettings
            //    {
            //        NullValueHandling = NullValueHandling.Ignore,
            //        Converters = new[] { new VendorExtensionsConverter() }
            //    }
            //);

            services.AddSwaggerGen(
                options =>
                {
                    var provider = services.BuildServiceProvider()
                                        .GetRequiredService<IApiVersionDescriptionProvider>();

                    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(
                            description.GroupName,
                            new Info()
                            {
                                Title = $"Wappa Challenge {description.ApiVersion}",
                                Version = description.ApiVersion.ToString(),
                                Contact = new Contact()
                                {
                                    Name = "Leo Lima",
                                    Email = "leolima@leolima77.com.br",
                                    Url = "https://leolima77.com.br"
                                }
                            });
                    }
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory,
            IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (errorFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                        }

                        await context.Response.WriteAsync("There was an error");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseCors("AllowAllOrigins");
            //AutoMapper.Mapper.Initialize(mapper =>
            //{
            //    mapper.CreateMap<FoodItem, FoodItemDto>().ReverseMap();
            //    mapper.CreateMap<FoodItem, FoodUpdateDto>().ReverseMap();
            //    mapper.CreateMap<FoodItem, FoodCreateDto>().ReverseMap();
            //});

            app.UseMvc();
        }
    }
}
