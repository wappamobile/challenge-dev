using AutoMapper;
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
    public class QueryDriverTest
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<QueryDriverHandler> _logger;
        private readonly QueryDriverHandler _handler;

        public QueryDriverTest()
        {
            _mapper = MapperHelper.GetMapper();

            _driverRepository = Substitute.For<IDriverRepository>();

            _logger = LoggerHelper.GetLogger<QueryDriverHandler>();

            _handler = new QueryDriverHandler(
                            _mapper,
                            _driverRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_GetVehicleAsync()
        {
            _driverRepository.GetSearchAsync(Arg.Any<IDriver>())
                  .Returns(new IDriver[]
                  {
                      DriverFake.GetDriver(),
                      DriverFake.GetDriver()
                  });

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
            Assert.NotNull(response.Value);
            var result = (IEnumerable<IDriver>)response.Value;
            Assert.True(result.Any());
        }

        [Fact]
        public async Task Should_NotGetVehiclesAsync()
        {
            _driverRepository.GetSearchAsync(Arg.Any<IDriver>())
                  .Returns(new Driver[] { });

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
            Assert.Null(response.Value);
        }

        private static QueryDriverRequest GetRequest() =>
            new QueryDriverRequest
            {
                Id = 1,
                Document = "12312312312",
                FirstName = "Márcio",
                LastName = "Adão"
            };
    }
}