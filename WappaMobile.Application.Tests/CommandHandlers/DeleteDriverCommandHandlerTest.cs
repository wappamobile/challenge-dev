using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WappaMobile.Domain;
using WappaMobile.Persistence;
using Xunit;

namespace WappaMobile.Application.Tests
{
    public class DeleteDriverCommandHandlerTest : IDisposable
    {
        private readonly DriverContext _driverContext;
        private readonly Guid _driverId;

        public DeleteDriverCommandHandlerTest()
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
            _driverContext.SaveChanges();

            _driverId = driver.DriverId;
        }

        public void Dispose()
        {
            _driverContext.Dispose();
        }

        [Fact]
        public async Task Handle_WithId_DeletesDriver()
        {
            // Arrange
            var handler = new DeleteDriverCommandHandler(_driverContext);
            var command = new DeleteDriverCommand(_driverId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(await _driverContext.Drivers.AnyAsync(d => d.DriverId == _driverId));
        }
    }
}
