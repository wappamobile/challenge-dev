using Autofac;
using AutoMapper;
using Wappa.Middleware.Application.Cars;
using Wappa.Middleware.Application.Drivers;
using Wappa.Middleware.Core.Cars;
using Wappa.Middleware.Core.Drivers;
using Wappa.Middleware.Core.GoogleMaps;
using Wappa.Middleware.EntityFrameworkCore.Contexts;
using Wappa.Middleware.EntityFrameworkCore.Repositories.Cars;
using Wappa.Middleware.EntityFrameworkCore.Repositories.Drivers;
using Wappa.Middleware.EntityFrameworkCore.UoW;
using Wappa.Middleware.Miscellaneous;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Wappa.Middleware.Autofac
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
            var mapper = mapperConfiguration.CreateMapper();
            builder.Register(c => mapper).As<IMapper>().SingleInstance();

            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var directoryInfo = Directory.GetCurrentDirectory();
            var builder1 = new ConfigurationBuilder()
                .SetBasePath(directoryInfo)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();

            builder.Register(c => builder1).As<IConfigurationRoot>().SingleInstance();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder
                .RegisterType<AppDbContext>()
                .WithParameter("options", DbContextOptionsBuilder.Get())
                .InstancePerLifetimeScope();

            builder.RegisterType<GoogleMapManager>().As<IGoogleMapManager>().InstancePerLifetimeScope();
            builder.RegisterType<DriverRepository>().As<IDriverRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DriverManager>().As<IDriverManager>().InstancePerLifetimeScope();
            builder.RegisterType<DriverAppService>().As<IDriverAppService>().InstancePerLifetimeScope();
            builder.RegisterType<CarRepository>().As<ICarRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CarManager>().As<ICarManager>().InstancePerLifetimeScope();
            builder.RegisterType<CarAppService>().As<ICarAppService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            
        }
    }
}
