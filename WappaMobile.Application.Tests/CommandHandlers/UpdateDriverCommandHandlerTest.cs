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
    public class UpdateDriverCommandHandlerTest : IDisposable
    {
        private readonly DriverContext _driverContext;
        private readonly Mapper _mapper;
        private readonly DummyGeocoder _geocoder;
        private readonly Guid _driverId;

        public UpdateDriverCommandHandlerTest()
        {
            var rnd = new Random();
            _driverContext = new DriverContext(new DbContextOptionsBuilder<DriverContext>()
                        .UseInMemoryDatabase("Test" + rnd.NextDouble().ToString())
                        .Options);
            var driver = new Driver
            {
                FirstName = "Fulano",
                LastName = "da Silva",
                Car = new Car
                {
                    Brand = "Ford",
                    Model = "Focus",
                    RegistrationPlate = "LUJ1234"
                },
                Address = new Address
                {
                    Line1 = "Av Brasil, 9020",
                    Line2 = "Olaria",
                    Municipality = "Rio de Janeiro",
                    State = "RJ",
                    ZipCode = "21030-001",
                    Coordinates = new Coordinates(-22.8370646, -43.2556345)
                }
            };
            _driverContext.Drivers.Add(driver);
            _driverContext.SaveChangesAsync();
            _driverId = driver.DriverId;

            _mapper = new Mapper(new MapperConfiguration(
                            cfg => cfg.AddProfile(new DomainProfile())));
            _geocoder = new DummyGeocoder(new Coordinates(20, -30));
        }

        public void Dispose()
        {
            _driverContext.Dispose();
        }

        [Fact]
        public async Task Handle_WithData_RefetchesCoordinatesAndUpdatesDriver()
        {
            // Arrange
            var handler = new UpdateDriverCommandHandler(_driverContext, _mapper, _geocoder);
            var command = new UpdateDriverCommand(_driverId, new ModifyDriverDto
            {
                FirstName = "Ciclano",
                LastName = "da Silva Souza",
                CarBrand = "Ford",
                CarModel = "Ecosport",
                CarRegistrationPlate = "LUJ321",
                AddressLine1 = "Av Brig Luiz Antonio, 411",
                AddressLine2 = "Bela Vista",
                AddressMunicipality = "São Paulo",
                AddressState = "SP",
                AddressZipCode = "01317-000"
            });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var driver = await _driverContext.Drivers.FirstAsync();
            Assert.Equal("Ciclano", driver.FirstName);
            Assert.Equal("da Silva Souza", driver.LastName);
            Assert.Equal("Ford", driver.Car.Brand);
            Assert.Equal("Ecosport", driver.Car.Model);
            Assert.Equal("LUJ321", driver.Car.RegistrationPlate);
            Assert.Equal("Av Brig Luiz Antonio, 411", driver.Address.Line1);
            Assert.Equal("Bela Vista", driver.Address.Line2);
            Assert.Equal("São Paulo", driver.Address.Municipality);
            Assert.Equal("SP", driver.Address.State);
            Assert.Equal("01317-000", driver.Address.ZipCode);
            Assert.Equal(20, driver.Address.Coordinates.Latitude);
            Assert.Equal(-30, driver.Address.Coordinates.Longitude);
        }
    }
}
