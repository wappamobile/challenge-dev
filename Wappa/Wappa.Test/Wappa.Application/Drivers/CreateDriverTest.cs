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
    public class CreateDriverTest
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<CreateDriverHandler> _logger;
        private readonly CreateDriverHandler _handler;

        public CreateDriverTest()
        {
            _mapper = MapperHelper.GetMapper();

            _driverRepository = Substitute.For<IDriverRepository>();

            _logger = LoggerHelper.GetLogger<CreateDriverHandler>();

            _handler = new CreateDriverHandler(
                            _mapper,
                            _driverRepository,
                            _logger);
        }

        [Fact]
        public async Task Should_ChangeAddressAsync()
        {
            _driverRepository.SaveAsync(Arg.Any<IDriver>())
                             .Returns(DriverFake.GetDriver());

            _driverRepository.HasDriverAsync(Arg.Any<string>())
                              .Returns(false);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotChangeAddressAsync()
        {
            _driverRepository.SaveAsync(Arg.Any<IDriver>())
                             .Returns(DriverFake.GetDriver());

            _driverRepository.HasDriverAsync(Arg.Any<string>())
                              .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotChangeAddressDataBaseErrorAsync()
        {
            _driverRepository.SaveAsync(Arg.Any<IDriver>())
                             .Returns((IDriver)null);

            _driverRepository.HasDriverAsync(Arg.Any<string>())
                              .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        private static CreateDriverRequest GetRequest()
        {
            return new CreateDriverRequest
            {
                Document = "12312312312",
                FirstName = "Márcio",
                LastName = "Adão",
            };
        }
    }
}