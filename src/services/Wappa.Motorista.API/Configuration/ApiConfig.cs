using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Wappa.Motoristas.API.Data;

namespace Wappa.Motoristas.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = "";

            if (Environment.GetEnvironmentVariable("CONTAINER") == "true")
                conn = configuration.GetConnectionString("Container");
            else
                conn = configuration.GetConnectionString("Localhost");

            services.AddDbContext<MotoristaContext>(options =>
                options.UseSqlServer(conn, m => m.MigrationsAssembly("Wappa.Motoristas.API")));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, MotoristaContext motoristaContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //motoristaContext.Database.Migrate();

            app.UseRouting();

            app.UseCors("Total");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}