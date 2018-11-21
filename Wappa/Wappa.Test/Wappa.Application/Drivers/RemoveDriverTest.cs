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
    public class RemoveDriverTest
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<RemoveDriverHandler> _logger;
        private readonly RemoveDriverHandler _handler;

        public RemoveDriverTest()
        {
            _mapper = MapperHelper.GetMapper();

            _driverRepository = Substitute.For<IDriverRepository>();

            _logger = LoggerHelper.GetLogger<RemoveDriverHandler>();

            _handler = new RemoveDriverHandler(
                            _mapper,
                            _driverRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_DeleteAddressAsync()
        {
            _driverRepository.HasDriverAsync(Arg.Any<int>())
                  .Returns(true);

            _driverRepository.DeleteAsync(Arg.Any<IDriver>())
                  .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotDeleteAddressAsync()
        {
            _driverRepository.HasDriverAsync(Arg.Any<int>())
                  .Returns(false);

            _driverRepository.DeleteAsync(Arg.Any<IDriver>())
                  .Returns(false);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        private static RemoveDriverRequest GetRequest() =>
            new RemoveDriverRequest(1);
    }
}