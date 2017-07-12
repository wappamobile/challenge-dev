using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DriverMgr.Api;
using DriverMgr.Api.Controllers;
using DriverMgr.Api.App_Start;
using DriverMgr.Api.Tests.Global;
using DriverMgr.TO;
using DriverMgr.DataAccess.Contracts;
using DriverMgr.DataAccess.Factory;
using DriverMgr.DataAccessMock;

namespace DriverMgr.Api.Tests.Controllers
{
    [TestClass]
    public class DriverControllerTest
    {
        public DataFactory Factory { get; set; }

        public DriverControllerTest()
        {
            DataFactoryProvider.Singleton = new DataFactoryProviderMock()
            {
                Factory = Factory = new DataFactoryMock()
            };
        }

        // As validações feitas nesse teste são bem simplificadas.
        // Foi feito somente alguns testes para demonstrar o conhecimento sobre o uso,
        // Porem em um cenário real, mais testes seriam adicionados e a complexidade dos
        // mesmos poderia ser almentada.

        [TestMethod]
        public void Get()
        {
            // Arrange
            var controller = new DriverController();

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("John", result.ElementAt(0).FirstName);
            Assert.AreEqual("Marry", result.ElementAt(1).FirstName);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var controller = new DriverController();

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.AreEqual(1, result.DriverId);
            Assert.AreEqual("John", result.FirstName);
            Assert.AreEqual("Doe", result.LastName);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var controller = new DriverController();

            var driver = new DriverTO
            {
                FirstName = "James",
                LastName = "Bond",
                ManufacturerId = 3,
                Model = "Martin",
                Plate = "BND-0007",
                Address = "3411 Las Vegas Blvd. So. Las Vegas, NV 89109",
                Latitude = 36.1206696,
                Longitude = -115.17168
            };

            // Act
            controller.Post(driver);

            // Assert
            var created = Factory
                .GetDAL<IDriverDAL>()
                .List()
                .Where(w => w.FirstName == "James" && w.LastName == "Bond")
                .FirstOrDefault();

            Assert.IsNotNull(created);
            Assert.AreEqual(3, created.DriverId);
            Assert.AreEqual("Martin", created.Model);
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            var controller = new DriverController();

            var driver = new DriverTO
            {
                DriverId = 1,
                FirstName = "James",
                LastName = "Bond",
                ManufacturerId = 3,
                Model = "Martin",
                Plate = "BND-0007",
                Address = "3411 Las Vegas Blvd. So. Las Vegas, NV 89109",
                Latitude = 36.1206696,
                Longitude = -115.17168
            };

            // Act
            controller.Put(driver.DriverId, driver);

            // Assert
            var updated = Factory
                .GetDAL<IDriverDAL>()
                .List()
                .Where(w => w.FirstName == "James" && w.LastName == "Bond")
                .FirstOrDefault();

            Assert.IsNotNull(updated);
            Assert.AreEqual(1, updated.DriverId);
            Assert.AreEqual("Martin", updated.Model);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var controller = new DriverController();

            // Act
            controller.Delete(1);

            // Assert
            var driver = Factory
                .GetDAL<IDriverDAL>()
                .List()
                .FirstOrDefault(f => f.DriverId == 1);

            Assert.IsNull(driver);
        }
    }
}
