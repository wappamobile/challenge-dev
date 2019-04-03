using System;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Domain.Commands;
using Wappa.Domain.Core.Bus;
using Wappa.Domain.Core.Notifications;
using Wappa.Domain.Events;
using Wappa.Domain.Interfaces;
using Wappa.Domain.Models;
using MediatR;

namespace Wappa.Domain.CommandHandlers
{
    public class DriverCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewDriverCommand, bool>,
        IRequestHandler<UpdateDriverCommand, bool>,
        IRequestHandler<RemoveDriverCommand, bool>
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMediatorHandler Bus;

        public DriverCommandHandler(IDriverRepository driverRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _driverRepository = driverRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewDriverCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var driver = new Driver()
            {
                Address = message.Address,
                CarBrand = message.CarBrand,
                CarModel = message.CarModel,
                CarPlate = message.CarPlate,
                LastName = message.LastName,
                Name = message.Name,
                Zipcode = message.Zipcode,
                Coordinates = message.Coordinates
            };

            _driverRepository.Add(driver);

            if (Commit())
            {
                Bus.RaiseEvent(new DriverRegisteredEvent(driver.Id, driver.Name, driver.LastName, driver.CarModel, driver.CarBrand,
                    driver.CarPlate, driver.Zipcode, driver.Address, driver.Coordinates));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateDriverCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var driver = new Driver(message.Id, message.Name, message.LastName, message.CarModel, message.CarBrand,
                message.CarPlate, message.Zipcode, message.Address, message.Coordinates);

            _driverRepository.Update(driver);

            if (Commit())
            {
                Bus.RaiseEvent(new DriverRegisteredEvent(driver.Id, driver.Name, driver.LastName, driver.CarModel, driver.CarBrand,
                    driver.CarPlate, driver.Zipcode, driver.Address, driver.Coordinates));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveDriverCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _driverRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new DriverRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _driverRepository.Dispose();
        }
    }
}