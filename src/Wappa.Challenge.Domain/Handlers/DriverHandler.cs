using System;
using AutoMapper;
using System.Linq;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Domain.Commands.Outputs;
using Wappa.Challenge.Domain.Models;
using Wappa.Challenge.Domain.Repositories;
using Wappa.Challenge.Domain.Services;
using Wappa.Challenge.Shared.Commands;

namespace Wappa.Challenge.Domain.Handlers
{
    public class DriverHandler :
        ICommandHandler<CreateDriverCommand>,
        ICommandHandler<DeleteDriverCommand>,
        ICommandHandler<UpdateDriverCommand>,
        ICommandHandler<ListDriversCommand>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGoogleService _googleService;

        public DriverHandler(IDriverRepository repository, IMapper mapper, IGoogleService googleService)
        {
            _repository = repository;
            _mapper = mapper;
            _googleService = googleService;
        }

        public ICommandResult Handle(CreateDriverCommand command)
        {
            var driver = _mapper.Map<CreateDriverCommand, Driver>(command);

            var coordinates = _googleService.GetCoordinates(command.Address);

            if (coordinates.longitude == null || coordinates.latitude == null)
            {
                return new CommandResult(
                    success: false,
                    message: "It was not possible find coordinates"
                );
            }

            driver.Id = Guid.NewGuid();
            driver.Address.SetCoordinates(coordinates);

            _repository.Insert(driver);
            
            return new CommandResult(
                success: true,
                message: "Driver created successfuly"
            );
        }

        public ICommandResult Handle(DeleteDriverCommand command)
        {
            if (!_repository.Exists(command.Id))
            {
                return new CommandResult(
                    success: false,
                    message: "Driver not found"
                );
            }

            _repository.Delete(command.Id);

            return new CommandResult(
                success: true,
                message: "Driver deleted successfuly"
            );
        }

        public ICommandResult Handle(UpdateDriverCommand command)
        {
            var driver = _mapper.Map<UpdateDriverCommand, Driver>(command);

            var coordinates = _googleService.GetCoordinates(command.Address);

            if (coordinates.longitude == null || coordinates.latitude == null)
            {
                return new CommandResult(
                    success: false,
                    message: "It was not possible find coordinates"
                );
            }

            driver.Address.SetCoordinates(coordinates);

            if (!_repository.Exists(driver.Id))
            {
                return new CommandResult(
                    success: false,
                    message: "Driver not found"
                );
            }

            _repository.Update(driver);

            return new CommandResult(
                success: true,
                message: "Driver updated successfuly"
            );
        }

        public ICommandResult Handle(ListDriversCommand command)
        {
            var drivers = _repository.List(command.OrderBy)
                                     .Select(d => _mapper.Map<Driver, DriverResult>(d))
                                     .ToList();

            return new ListDriversCommandResult(
                success: true,
                message: "",
                drivers: drivers
            );
        }
    }
}