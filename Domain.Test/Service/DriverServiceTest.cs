using System.Net.Http;
using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Domain.Test.Service
{
    public class DriverServiceTest
    {
        readonly Mock<ChallengeDevEntityContext> mockContext = new Mock<ChallengeDevEntityContext>();
        readonly Mock<IGeocodingService> mockGeocodingService = new Mock<IGeocodingService>();
        readonly Mock<IGeocodingRepository> mockGeocodingRepository = new Mock<IGeocodingRepository>();
        readonly Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
        readonly string mockedGeocodingApiKey = "AIzaSyA1k0SUXcYfM4IjeTZQSpasHk7_BU9bcU8";

        readonly Driver validDriver = new Driver()
        {
            FirstName = "Danilo",
            LastName = "Oliveira",
            Address = new Address(){
                City = "São Paulo",
                State = "São Paulo",
                Complement = "Casa 01",
                Country = "Brasil",
                Neighborhood = "Vila Paranaguá",
                Number = 93,
                Street = "Rua Victoria Simionato",
                ZipCode = "03808-170"
            },
            Car = new Car(){
                Brand = "Chevrolet",
                Model = "Celta",
                Plate = "EBI-0967"
            }
        };

        [Fact]
        public void ShouldPersistDriver()
        {
            var mockSet = new Mock<DbSet<Driver>>();
            mockContext.Setup(m => m.Drivers).Returns(mockSet.Object);
            mockGeocodingService.Setup(m => m.GeocodingByAddress(mockHttpClientFactory.Object, mockGeocodingRepository.Object, mockedGeocodingApiKey, validDriver.Address)).Returns(validDriver.Address);

            var service = new DriverService();
            service.Save(new LoggerFactory(), mockContext.Object, mockHttpClientFactory.Object, mockGeocodingRepository.Object, mockGeocodingService.Object, mockedGeocodingApiKey, validDriver);
            mockSet.Verify(m => m.Add(It.IsAny<Driver>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ShouldNotPersistDriver()
        {
            var mockSet = new Mock<DbSet<Driver>>();
            mockContext.Setup(m => m.Drivers).Returns(mockSet.Object);

            var service = new DriverService();
            service.Save(new LoggerFactory(), mockContext.Object, mockHttpClientFactory.Object, mockGeocodingRepository.Object, mockGeocodingService.Object, mockedGeocodingApiKey, new Driver(){});
            mockSet.Verify(m => m.Add(It.IsAny<Driver>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}
