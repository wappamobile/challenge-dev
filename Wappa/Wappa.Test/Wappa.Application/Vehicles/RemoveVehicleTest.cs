using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandHandlers;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Repositories;
using Wappa.Test.Wappa.Infra;
using Xunit;

namespace Wappa.Test.Wappa.Application.Addresses
{
    public class RemoveVehicleTest
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<RemoveVehicleHandler> _logger;
        private readonly RemoveVehicleHandler _handler;

        public RemoveVehicleTest()
        {
            _mapper = MapperHelper.GetMapper();

            _vehicleRepository = Substitute.For<IVehicleRepository>();

            _logger = LoggerHelper.GetLogger<RemoveVehicleHandler>();

            _handler = new RemoveVehicleHandler(
                            _mapper,
                            _vehicleRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_DeleteAddressAsync()
        {
            _vehicleRepository.DeleteAsync(Arg.Any<IVehicle>())
                  .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
        }

        private static RemoveVehicleRequest GetRequest() =>
            new RemoveVehicleRequest(1, 1);
    }
}