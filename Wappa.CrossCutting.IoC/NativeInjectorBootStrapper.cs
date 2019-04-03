using Microsoft.Extensions.DependencyInjection;
using Wappa.Domain.Core.Bus;
using Wappa.Infra.CrossCutting.Bus;
using Wappa.Application.Services;
using Wappa.Application.Interfaces;
using Wappa.Domain.Core.Notifications;
using MediatR;
using Wappa.Domain.Events;
using Wappa.Domain.EventHandlers;
using Wappa.Domain.CommandHandlers;
using Wappa.Domain.Commands;
using Wappa.Domain.Interfaces;
using Wappa.Infra.Data.UoW;
using Wappa.Infra.Data.Repository;
using Wappa.Infra.Data.Context;
using Wappa.Infra.GMaps;
using Wappa.Infra.Data.EventSourcing;
using Wappa.Infra.Data.Repository.EventSourcing;
using Wappa.Domain.Core.Events;

namespace Wappa.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, string apiKey)
        {
            
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IDriverAppService, DriverAppService>();
            services.AddScoped<IGMapsAppService, GMapsAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<DriverRegisteredEvent>, DriverEventHandler>();
            services.AddScoped<INotificationHandler<DriverUpdatedEvent>, DriverEventHandler>();
            services.AddScoped<INotificationHandler<DriverRemovedEvent>, DriverEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewDriverCommand, bool>, DriverCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDriverCommand, bool>, DriverCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveDriverCommand, bool>, DriverCommandHandler>();

            // Infra - Data
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<WappaContext>();
            services.AddScoped<IGMaps, Gmaps>(g => new Gmaps(apiKey));

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

        }
    }
}
