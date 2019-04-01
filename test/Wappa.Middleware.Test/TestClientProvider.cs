using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using Wappa.Middleware.Application.Cars;
using Wappa.Middleware.Application.Drivers;
using Wappa.Middleware.Consts;
using Wappa.Middleware.Core.Cars;
using Wappa.Middleware.Core.Drivers;
using Wappa.Middleware.Core.Extensions;
using Wappa.Middleware.Core.GoogleMaps;
using Wappa.Middleware.Domain.Configuration;
using Wappa.Middleware.EntityFrameworkCore.Contexts;
using Wappa.Middleware.EntityFrameworkCore.Repositories.Cars;
using Wappa.Middleware.EntityFrameworkCore.Repositories.Drivers;
using Wappa.Middleware.EntityFrameworkCore.UoW;
using Wappa.Middleware.Miscellaneous;

namespace Wappa.Middleware.Test
{
    public class TestClientProvider
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public HttpClient Client { get; set; }

        public TestClientProvider()
        {
            var serviceCollection = new ServiceCollection();

            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = AppConfig.Get(Directory.GetCurrentDirectory(), envName);
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            

            serviceCollection
                .AddEntityFrameworkSqlServer()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(AppConsts.ConnectionStringName)),
                    ServiceLifetime.Scoped);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            var mapper = config.CreateMapper();
            serviceCollection.AddSingleton(mapper);

            var googleOptions = new GoogleMapsConfiguration();
            new ConfigureFromConfigurationOptions<GoogleMapsConfiguration>(
                configuration.GetSection("GoogleMapsConfigurations"))
                    .Configure(googleOptions);

            serviceCollection.AddGoogleMaps(ops =>
            {
                ops.BaseUrl = googleOptions.BaseUrl;
                ops.ApiUrlAddress = googleOptions.ApiUrlAddress;
                ops.Key = googleOptions.Key;
            });

            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddTransient<IGoogleMapManager, GoogleMapManager>();
            serviceCollection.AddTransient<IDriverRepository, DriverRepository>();
            serviceCollection.AddTransient<IDriverManager, DriverManager>();
            serviceCollection.AddTransient<IDriverAppService, DriverAppService>();

            serviceCollection.AddTransient<ICarRepository, CarRepository>();
            serviceCollection.AddTransient<ICarManager, CarManager>();
            serviceCollection.AddTransient<ICarAppService, CarAppService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
