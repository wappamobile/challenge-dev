using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using Wappa.Contracts;
using Wappa.ChallengeDev.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Wappa.Contracts.Models;

namespace Wappa.Tests
{
    [TestFixture]
    public class DriverControllerTests
    {
        public Mock<IDriverDB> _driverDB = new Mock<IDriverDB>();
        public Mock<IGeoLocator> _geoLocator = new Mock<IGeoLocator>();
        public DriverController driverController;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Can_Delete_Driver_With_Existing_Id()
        {
            // Arrange

            string id = "123456";
            SetupForDelete(id);

            // Act 
            var result = new DriverController(_driverDB.Object, _geoLocator.Object).Delete(id).Result;
            // Assert
            _driverDB.Verify(x => x.DeleteDriver(id));
            Assert.IsInstanceOf<ContentResult>(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Cannot_Delete_Driver_With_Non_Existing_Id()
        {
            // Arrange

            string id = "987654";
            SetupForDelete(id);

            var result = new DriverController(_driverDB.Object, _geoLocator.Object).Delete(id).Result;
            // Assert
            _driverDB.Verify(x => x.DeleteDriver(id));
            Assert.IsInstanceOf<ContentResult>(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Can_Save_Driver()
        {
            // Arrange

            Driver d = new Driver
            {
                FirstName = "Samuel",
                LastName = "Camilo",
                Car = new Car { Manufacturer = "VW", Model = "Fox", Plate = "AAA-1234"},
                Address = new Address
                {
                    StreetName = "Avenida Francisco Rodrigues",
                    Number = "610",
                    AddressComplement = "Apto. 03",
                    District = "Jaçanã",
                    City = "São Paulo",
                    State = "São Paulo",
                    Country = "Brasil",
                    ZipCode = "02259-001"
                }                
            };

            SetupForSave();

            var result = new DriverController(_driverDB.Object, _geoLocator.Object).Post(d).Result;
            // Assert
            _driverDB.Verify(x => x.SaveDriver(d));
            Assert.IsInstanceOf<ContentResult>(result);
            Assert.AreEqual(201, result.StatusCode);
        }

        [Test]
        public void Can_Update_Driver()
        {
            // Arrange

            Driver d = new Driver
            {
                Id = "123456",
                FirstName = "Samuel",
                LastName = "Camilo",
                Car = new Car { Manufacturer = "VW", Model = "Fox", Plate = "AAA-1234" },
                Address = new Address
                {
                    StreetName = "Avenida Francisco Rodrigues",
                    Number = "610",
                    AddressComplement = "Apto. 03",
                    District = "Jaçanã",
                    City = "São Paulo",
                    State = "São Paulo",
                    Country = "Brasil",
                    ZipCode = "02259-001"
                }
            };

            SetupForSave();

            var result = new DriverController(_driverDB.Object, _geoLocator.Object).Put(d).Result;
            // Assert
            _driverDB.Verify(x => x.UpdateDriver(d));
            Assert.IsInstanceOf<ContentResult>(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        public void SetupForDelete(string id)
        {
            _driverDB.Setup(x => x.DeleteDriver(id))
                .Returns(() => 
                {
                    if (id == "123456")
                        return Task.CompletedTask;
                    else
                        throw new Exception("Driver not found");

                }).Verifiable();
        }

        public void SetupForSave()
        {
            _driverDB.Setup(x => x.SaveDriver(It.IsAny<Driver>()))
                .Returns(Task.CompletedTask).Verifiable();
        }

        public void SetupForUpdate()
        {
            _driverDB.Setup(x => x.UpdateDriver(It.IsAny<Driver>()))
                .Returns(Task.CompletedTask).Verifiable();
        }

    }
}
