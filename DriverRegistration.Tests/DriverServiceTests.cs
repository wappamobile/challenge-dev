using DriverRegistration.Data.MongoDb.Repositories;
using DriverRegistration.Domain.Entities;
using DriverRegistration.Domain.Services;
using DriverRegistration.Tests.Mocks;
using DriverRegistration.WebApi.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DriverRegistration.Tests
{
    public class DriverServiceTests
    {
        [Fact]
        public async Task Create_Driver()
        {
            var driver = new Driver
            {
                FirstName = "Lucas",
                LastName = "Rocha",
                Car = new Car
                {
                    Brand = "Volkswagen",
                    Model = "Gol 2013 1.0",
                    Plate = "PWC-8874"
                },
                Address = new Address
                {
                    Street = "Rua Pamplona",
                    Number = "976",
                    City = "São Paulo",
                    State = "SP",
                    ZipCode = "01405-200"
                }
            };

            var driverService = new DriverService(new DriverRepository(), new MapsServiceMock());

            await driverService.Insert(driver);

            var driverRetrived = driverService.Get(driver.Id);

            Assert.Equal(driver.Id, driverRetrived.Id);
            Assert.Equal(driver.FirstName, driverRetrived.FirstName);
            Assert.Equal(driver.Address.MapsLatitude, driverRetrived.Address.MapsLatitude);
        }

        [Fact]
        public async Task Update_Driver()
        {
            var driver = new Driver
            {
                FirstName = "João",
                LastName = "Das Neves",
                Car = new Car
                {
                    Brand = "Porsche",
                    Model = "911 Carrera 2016",
                    Plate = "CPW-8821"
                },
                Address = new Address
                {
                    Street = "Alameda Santos",
                    Number = "1235",
                    City = "São Paulo",
                    State = "SP",
                    ZipCode = "01419-002"
                }

            };

            var driverService = new DriverService(new DriverRepository(), new MapsServiceMock());

            var driverLast = driverService.GetAll(false, false).LastOrDefault();

            driver.Id = driverLast.Id;
            await driverService.Update(driverLast.Id, driver);

            var driverRetrived = driverService.Get(driverLast.Id);

            Assert.Equal(driverLast.Id, driverRetrived.Id);
            Assert.Equal(driver.FirstName, driverRetrived.FirstName);
        }

        [Fact]
        public async Task Delete_Driver_Object()
        {
            var driverService = new DriverService(new DriverRepository(), new MapsServiceMock());

            var driverFirst = driverService.GetAll(false, false).FirstOrDefault();
            
            await driverService.Delete(driverFirst);

            var driverRetrived = driverService.Get(driverFirst.Id);

            Assert.Null(driverRetrived);
        }

        [Fact]
        public async Task Delete_Driver_Id()
        {
            var driverService = new DriverService(new DriverRepository(), new MapsServiceMock());

            var driverFirst = driverService.GetAll(false, false).FirstOrDefault();

            await driverService.Delete(driverFirst.Id);

            var driverRetrived = driverService.Get(driverFirst.Id);

            Assert.Null(driverRetrived);
        }
    }
}
