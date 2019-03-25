using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WappaMobile.Domain;
using WappaMobile.Persistence;
using Xunit;

namespace WappaMobile.Application.Tests
{
    public class CreateDriverCommandHandlerTest : IDisposable
    {
        private readonly DriverContext _driverContext;
        private readonly Mapper _mapper;
        private readonly DummyGeocoder _geocoder;

        public CreateDriverCommandHandlerTest()
        {
            var rnd = new Random();
            _driverContext = new DriverContext(new DbContextOptionsBuilder<DriverContext>()
                        .UseInMemoryDatabase("Test" + rnd.NextDouble().ToString())
                        .Options);
            _mapper = new Mapper(new MapperConfiguration(
                            cfg => cfg.AddProfile(new DomainProfile())));
            _geocoder = new DummyGeocoder(new Coordinates(10, -10));
        }

        public void Dispose()
        {
            _driverContext.Dispose();
        }

        [Fact]
        public async Task Handle_WithData_FetchesCoorinatesAndCreatesDriver()
        {
            // Arrange
            var handler = new CreateDriverCommandHandler(_driverContext, _mapper, _geocoder);
            var command = new CreateDriverCommand(new ModifyDriverDto
            {
                FirstName = "Fulano",
                LastName = "da Silva",
                CarBrand = "Ford",
                CarModel = "Focus",
                CarRegistrationPlate = "LUJ1234",
                AddressLine1 = "Av Brasil, 9020",
                AddressLine2 = "Olaria",
                AddressMunicipality = "Rio de Janeiro",
                AddressState = "RJ",
                AddressZipCode = "21030-001"
            });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var driver = await _driverContext.Drivers.FirstAsync();
            Assert.Equal("Fulano", driver.FirstName);
            Assert.Equal("da Silva", driver.LastName);
            Assert.Equal("Ford", driver.Car.Brand);
            Assert.Equal("Focus", driver.Car.Model);
            Assert.Equal("LUJ1234", driver.Car.RegistrationPlate);
            Assert.Equal("Av Brasil, 9020", driver.Address.Line1);
            Assert.Equal("Olaria", driver.Address.Line2);
            Assert.Equal("Rio de Janeiro", driver.Address.Municipality);
            Assert.Equal("RJ", driver.Address.State);
            Assert.Equal("21030-001", driver.Address.ZipCode);
            Assert.Equal(10, driver.Address.Coordinates.Latitude);
            Assert.Equal(-10, driver.Address.Coordinates.Longitude);
        }
    }
}
