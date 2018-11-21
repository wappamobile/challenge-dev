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
    public class QueryAddressTest
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<QueryAddressHandler> _logger;
        private readonly QueryAddressHandler _handler;

        public QueryAddressTest()
        {
            _addressRepository = Substitute.For<IAddressRepository>();

            _logger = LoggerHelper.GetLogger<QueryAddressHandler>();

            _handler = new QueryAddressHandler(
                            _addressRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_GetAddressAsync()
        {
            _addressRepository.GetByDriverIdAsync(Arg.Any<int>())
                  .Returns(new IAddress[]
                                { AddressFake.GetAddress(),
                                  AddressFake.GetAddress()
                                });

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
            Assert.NotNull(response.Value);
            var result = (IEnumerable<IAddress>)response.Value;
            Assert.True(result.Any());
        }

        [Fact]
        public async Task Should_NotGetAddressAsync()
        {
            _addressRepository.GetByDriverIdAsync(Arg.Any<int>())
                  .Returns(new Address[] { });

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
            Assert.Null(response.Value);
        }

        private static QueryAddressRequest GetRequest() =>
            new QueryAddressRequest(1);
    }
}