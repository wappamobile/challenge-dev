using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Wappa.Domain.UnitOfWork;
using Wappa.Infra.UnitOfWork;
using Wappa.Service.GeometryService;
using Wappa.WebApi.ViewModels.Request;
using Wappa.WebApi.ViewModels.Validator;

namespace Wappa.WebApi
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
            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>(impl => new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IValidator<PostMotoristasRequest>, PostMotoristasRequestValidator>();

            services.AddSingleton<IGeometryServiceAsync, GeometryServiceAsync>(impl =>
            {
                return new GeometryServiceAsync(
                    endpoint: Configuration.GetValue<string>("MapsApi:Endpoint"),
                    apiKey: Configuration.GetValue<string>("MapsApi:ApiKey"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Wappa API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wappa API V1");
            });

            app.UseMvc();
        }
    }
}
