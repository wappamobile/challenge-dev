using Autofac;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Vitor.CrossCutting
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new ConfigurationBuilder()
                                      .SetBasePath(Directory.GetCurrentDirectory())
                                      .AddJsonFile("appsettings.json", true, false)
                                      .AddEnvironmentVariables()
                                      .Build())
                   .As<IConfiguration>()                   
                   .SingleInstance()
                   .AutoActivate();
        }
    }
}
