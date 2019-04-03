using System.Threading;
using System.Threading.Tasks;
using Wappa.Domain.Events;
using MediatR;

namespace Wappa.Domain.EventHandlers
{
    public class DriverEventHandler :
        INotificationHandler<DriverRegisteredEvent>,
        INotificationHandler<DriverUpdatedEvent>,
        INotificationHandler<DriverRemovedEvent>
    {
        public Task Handle(DriverUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(DriverRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(DriverRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}