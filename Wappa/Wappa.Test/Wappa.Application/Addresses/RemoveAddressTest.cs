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
    public class RemoveAddressTest
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<RemoveAddressHandler> _logger;
        private readonly RemoveAddressHandler _handler;

        public RemoveAddressTest()
        {
            _mapper = MapperHelper.GetMapper();

            _addressRepository = Substitute.For<IAddressRepository>();

            _logger = LoggerHelper.GetLogger<RemoveAddressHandler>();

            _handler = new RemoveAddressHandler(
                            _mapper,
                            _addressRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_DeleteAddressAsync()
        {
            _addressRepository.DeleteAsync(Arg.Any<IAddress>())
                  .Returns(true);
            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
        }

        private static RemoveAddressRequest GetRequest() =>
            new RemoveAddressRequest(1, 1);
    }
}