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
    public class ChangeDriverTest
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<ChangeDriverHandler> _logger;
        private readonly ChangeDriverHandler _handler;

        public ChangeDriverTest()
        {
            _mapper = MapperHelper.GetMapper();

            _driverRepository = Substitute.For<IDriverRepository>();

            _logger = LoggerHelper.GetLogger<ChangeDriverHandler>();

            _handler = new ChangeDriverHandler(
                            _mapper,
                            _driverRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_ChangeAddressAsync()
        {
            _driverRepository.SaveAsync(Arg.Any<IDriver>())
                 .Returns(DriverFake.GetDriver());

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotChangeAddressAsync()
        {
            _driverRepository.SaveAsync(Arg.Any<IDriver>())
                 .Returns(DriverFake.GetDriver());

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(false);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotChangeAddressDataBaseErrorAsync()
        {
            _driverRepository.SaveAsync(Arg.Any<IDriver>())
                 .Returns((IDriver)null);

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        private static ChangeDriverRequest GetRequest()
        {
            return new ChangeDriverRequest
            {
                Id = 1,
                FirstName = "Márcio",
                LastName = "Adão",
            };
        }
    }
}