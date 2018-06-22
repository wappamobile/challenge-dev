using Autofac;
using Microsoft.Extensions.Configuration;
using Vitor.Application;
using Vitor.Application.Options;
using Vitor.Domain.Repository;
using Vitor.Domain.Service;
using Vitor.Infrastructure;

namespace Vitor.CrossCutting
{
    public class VitorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoogleMapsOptions>().As<GoogleMapsOptions>();
            builder.RegisterType<PostgresOptions>().As<PostgresOptions>();
            builder.Register(ctx => new DriverRepository(ctx.Resolve<PostgresOptions>())).As<IDriverRepository>();
            builder.Register(ctx => new DriverService(ctx.Resolve<IDriverRepository>(), ctx.Resolve<GoogleMapsOptions>())).As<IDriverService>();
        
            base.Load(builder);
        }
    }
}
