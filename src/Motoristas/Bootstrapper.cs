using Flurl.Http.Configuration;
using GoogleGeolocationService;
using MediatR;
using MicroservicesPlatform.Configuration;
using Microsoft.Extensions.Configuration;
using Motoristas.Config;
using Motoristas.Core;
using Motoristas.Core.Data;
using Motoristas.Core.Data.MongoDB;
using Motoristas.Core.Services;
using Motoristas.Handlers;
using Motoristas.Handlers.Commands;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoristas
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly ILogger _log;
        private readonly IConfiguration _configuration;

        public Bootstrapper(ILogger log, IConfiguration configuration)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.OnError += (ctx, ex) =>
            {
                _log.Error(ex, "Unhandled exception");
                return null;
            };
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            var dbConfig = _configuration.GetConfig<DatabaseConfig>("Database");
            var googleServiceConfig = _configuration.GetConfig<GeolocationServiceConfig>("GoogleGeoService");

            container.Register(_log);
            container.Register<IMediator>(new Mediator(container.Resolve));
            container.Register<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
            container.Register<IPerfilMotoristaDbContext>(new PerfilMotoristaDbContext(dbConfig.ConnectionString));
            container.Register<IPerfilMotoristaRepository , PerfilMotoristaRepository>();
            container.Register<IIdentityGenerator<string>, MongoDbIdentityGenerator>();

            container.Register<IGeolocationService>(new GeolocationService(new GoogleClientConfig(googleServiceConfig.Address, googleServiceConfig.Key), _log));

            // TinyIoC não suporta open generics...
            container.Register(typeof(IRequestHandler<,>));
            container.Register<IRequestHandler<ObterPerfilMotorista, ObterPerfilMotoristaResponse>, PerfilMotoristaRequestHandler>();
            container.Register<IRequestHandler<PerfilMotoristaQuery, PerfilMotoristaQueryResponse>, PerfilMotoristaRequestHandler>();
            container.Register<IRequestHandler<RegistrarPerfilMotorista, RegistrarPerfilMotoristaResponse>, PerfilMotoristaRequestHandler>();
            container.Register<IRequestHandler<RemoverPerfilMotorista, RemoverPerfilMotoristaResponse>, PerfilMotoristaRequestHandler>();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var httpClientFactory = container.Resolve<IFlurlClientFactory>();
        }
    }
}
