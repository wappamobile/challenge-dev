using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wappa.Driver.Api;
using Wappa.Driver.Api.Data.Context;
using Wappa.Driver.Api.Dtos;
using Wappa.Driver.Api.Repositories.Implementations;
using Wappa.Driver.Api.Repositories.Interfaces;
using Wappa.Driver.Api.Services.Implementations;
using Wappa.Driver.Api.Services.Interfaces;

namespace Wappa.Driver.Tests
{
    class StartupTest
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var appSettings = new AppSettings("", "AIzaSyCvc3G4hOO1mmlmM4ENoeu7b70R_NbW_pI");
            services.AddScoped(serv => appSettings);
            services.AddScoped<IMapsService, MapsService>();
            services.AddScoped<IDriverRepository<DriverDto>, DriverRepository>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContextPool<DriverContext>(opt =>
            {
                opt.UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                app.UseMvc();
            }
        }
    }
}
