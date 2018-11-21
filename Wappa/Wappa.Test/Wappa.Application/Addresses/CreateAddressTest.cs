using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandHandlers;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Interfaces.Connectors;
using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Models;
using Wappa.Domain.Repositories;
using Wappa.Test.Wappa.Domain;
using Wappa.Test.Wappa.Infra;
using Xunit;

namespace Wappa.Test.Wappa.Application.Addresses
{
    public class CreateAddressTest
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<CreateAddressHandler> _logger;
        private readonly IGoogleMapsConnector _googleMaps;

        private readonly CreateAddressHandler _handler;

        public CreateAddressTest()
        {
            _mapper = MapperHelper.GetMapper();

            _addressRepository = Substitute.For<IAddressRepository>();

            _driverRepository = Substitute.For<IDriverRepository>();

            _logger = LoggerHelper.GetLogger<CreateAddressHandler>();

            _googleMaps = Substitute.For<IGoogleMapsConnector>();

            _googleMaps.GetLocationAsync(Arg.Any<IAddress>())
                       .Returns(new Location(-10000, -10000));

            _handler = new CreateAddressHandler(
                            _mapper,
                            _addressRepository,
                            _driverRepository,
                            _logger,
                            _googleMaps);
        }

        [Fact]
        public async Task Should_CreateAddressAsync()
        {
            _addressRepository.SaveAsync(Arg.Any<IAddress>())
                  .Returns(AddressFake.GetAddress());

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.False(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotCreateAddressAsync()
        {
            _addressRepository.SaveAsync(Arg.Any<IAddress>())
                  .Returns(AddressFake.GetAddress());

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                              .Returns(false);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        [Fact]
        public async Task Should_NotCreateAddressDatabaSeErrorAsync()
        {
            _addressRepository.SaveAsync(Arg.Any<IAddress>())
                  .ReturnsForAnyArgs((IAddress)null);

            _driverRepository.HasDriverAsync(Arg.Any<int>())
                             .Returns(true);

            var response = await _handler.Handle(GetRequest(), CancellationToken.None);

            Assert.True(response.HasMessages);
        }

        private static CreateAddressRequest GetRequest()
        {
            return new CreateAddressRequest
            {
                DriverId = 1,
                PostalCode = "03937090",
                StreetName = "Avenida Ouro Verde de Minas",
                Number = "800",
                Neighborhood = "Jardim Imperador",
                City = "São Paulo",
                StateCode = "SP",
                Country = "Brasil"
            };
        }
    }
}