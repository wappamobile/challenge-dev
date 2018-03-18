using Microsoft.Extensions.Configuration;
using System;
using Wappa.DataAccess;
using Wappa.Models;
using Xunit;
using System.Linq;
using Moq;
using Wappa.DataAccess.Contracts;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Wappa.Application;
using Wappa.ChallengeDev.Controllers;

namespace Wappa.Tests
{
    public class TaxistaControllerTests
    {

        [Fact]
        public void Can_Insert_Valid_Data()
        {
            var taxista = new Taxista
            {
                Marca = "VW",
                Modelo = "Santana",
                PrimeiroNome = "Augustinho",
                UltimoNome = "Carrara",
                Placa = "XYZ-9876"
            };
            // Arrange - create mock repository
            Mock<ITaxistaFacade> target = new Mock<ITaxistaFacade>();
            // Arrange - create mock temp data 
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var controller = new TaxistaController(target.Object) { TempData = tempData.Object };
            // Act 
            IActionResult result = controller.Edit(taxista).Result;
            // Assert
            target.Verify(m => m.Insert(taxista));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);

        }

        [Fact]
        public void Can_Update_Valid_Data()
        {
            var taxista = new Taxista
            {
                Marca = "VW",
                Modelo = "Santana",
                PrimeiroNome = "Augustinho",
                UltimoNome = "Carrara",
                Placa = "XYZ-9876",
                IdTaxista = 1
            };
            // Arrange - create mock repository
            Mock<ITaxistaFacade> target = new Mock<ITaxistaFacade>();
            // Arrange - create mock temp data 
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var controller = new TaxistaController(target.Object) { TempData = tempData.Object };
            // Act 
            IActionResult result = controller.Edit(taxista).Result;
            // Assert
            target.Verify(m => m.Update(taxista));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);

        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<ITaxistaFacade> mock = new Mock<ITaxistaFacade>();
            // Arrange - create the controller
            TaxistaController target = new TaxistaController(mock.Object);
            // Arrange 
            var taxista = new Taxista { PrimeiroNome = "Test", IdTaxista = 1 };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act
            IActionResult result = target.Edit(taxista).Result;

            // Assert - check that the repository was not called
            mock.Verify(m => m.Update(It.IsAny<Taxista>()), Times.Never());
            // Assert - check the method result type
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void Can_Delete_Valid_Data()
        {
            // Arrange 
            var taxista = new Taxista { IdTaxista = 2, PrimeiroNome = "Test" };

            // Arrange - create the mock repository
            Mock<ITaxistaFacade> mock = new Mock<ITaxistaFacade>();
            mock.Setup(m => m.List()).Returns(new Taxista[] {
                taxista,
                new Taxista {IdTaxista = 3, PrimeiroNome = "T2"},
            }.ToList());

            // Arrange - create the controller
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            TaxistaController target = new TaxistaController(mock.Object) { TempData = tempData.Object };

            // Act
            target.Remove(taxista.IdTaxista.Value);

            // Assert
            mock.Verify(m => m.Delete(taxista.IdTaxista.Value));
        }
    }
}
