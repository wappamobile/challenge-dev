using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandHandlers;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Models;
using Wappa.Domain.Repositories;
using Wappa.Test.Wappa.Domain;
using Wappa.Test.Wappa.Infra;
using Xunit;

namespace Wappa.Test.Wappa.Application.Addresses
{
    public class QueryVehicleTest
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<QueryVehicleHandler> _logger;
        private readonly QueryVehicleHandler _handler;

        public QueryVehicleTest()
        {
            _vehicleRepository = Substitute.For<IVehicleRepository>();

            _logger = LoggerHelper.GetLogger<QueryVehicleHandler>();

            _handler = new QueryVehicleHandler(
                            _vehicleRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_GetVehicleAsync()
        {
            _vehicleRepository.GetByDriverIdAsync(Arg.Any<int>())
                  .Returns(new IVehicle[]
                  {
                      VehicleFake.GetVehicle(),
                      VehicleFake.GetVehicle()
                  });

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
            Assert.NotNull(response.Value);
            var result = (IEnumerable<IVehicle>)response.Value;
            Assert.True(result.Any());
        }

        [Fact]
        public async Task Should_NotGetVehiclesAsync()
        {
            _vehicleRepository.GetByDriverIdAsync(Arg.Any<int>())
                  .Returns(new Vehicle[] { });

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
            Assert.Null(response.Value);
        }

        private static QueryVehicleRequest GetRequest() =>
            new QueryVehicleRequest(1);
    }
}