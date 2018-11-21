using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandHandlers;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Repositories;
using Wappa.Test.Wappa.Domain;
using Wappa.Test.Wappa.Infra;
using Xunit;

namespace Wappa.Test.Wappa.Application.Addresses
{
    public class CreateVehicleTest
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<CreateVehicleHandler> _logger;
        private readonly CreateVehicleHandler _handler;

        public CreateVehicleTest()
        {
            _mapper = MapperHelper.GetMapper();

            _vehicleRepository = Substitute.For<IVehicleRepository>();

            _driverRepository = Substitute.For<IDriverRepository>();

            _logger = LoggerHelper.GetLogger<CreateVehicleHandler>();

            _handler = new CreateVehicleHandler(
                            _mapper,
                            _vehicleRepository,
                            _driverRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_CreateAddressAsync()
        {
            _vehicleRepository.SaveAsync(Arg.Any<IVehicle>())
                  .Returns(VehicleFake.GetVehicle());

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotCreateAddressAsync()
        {
            _vehicleRepository.SaveAsync(Arg.Any<IVehicle>())
                  .Returns(VehicleFake.GetVehicle());

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(false);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotCreateAddressDatabaseErrorAsync()
        {
            _vehicleRepository.SaveAsync(Arg.Any<IVehicle>())
                  .Returns((IVehicle)null);

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        private static CreateVehicleRequest GetRequest()
        {
            return new CreateVehicleRequest
            {
                DriverId = 1,
                Plate = "XXX1234",
                Model = "Clio",
                Fabricator = "Renault",
            };
        }
    }
}