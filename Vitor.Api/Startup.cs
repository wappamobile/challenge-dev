using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Vitor.CrossCutting;
using Vitor.Common;

namespace Vitor.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Register<IConfiguration>(x => Configuration);
            builder.Populate(services);

            var appContainer = InitializeService(builder);
            return appContainer.Resolve<IServiceProvider>();
        }

        public virtual IContainer InitializeService(ContainerBuilder builder)
        {
            var crossCuttingAssembly = typeof(VitorModule).GetTypeInfo().Assembly;
            var configurationAssembly = typeof(ConfigurationModule).GetTypeInfo().Assembly;

            var options = new Vitor.Common.InitializeOptions(new[] { crossCuttingAssembly, configurationAssembly }, ContainerBuildOptions.None);
            return IoCWrapper.InitializeContainer(options, builder);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
