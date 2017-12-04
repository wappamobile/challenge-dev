using Wappa.Domain.UnitOfWork;
using Xunit;
using Moq;
using Wappa.Domain.Entities;
using Wappa.WebApi.Controllers;
using Wappa.WebApi.ViewModels.Common;

namespace Wappa.Tests
{
    public class CarrosUnitTest
    {
        [Fact]
        public void GetAll()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(repo => repo.GetCarroRepository().ListarTodos()).Returns(listarTodosCarrosSuccess());
            var controller = new CarrosController(unitOfWorkMock.Object);

            // Act
            var model = controller.Get();

            // Assert
            Assert.Equal(2, model.Count);
            Assert.IsType<CarroViewModel[]>(model.Items);
        }

        private Carro[] listarTodosCarrosSuccess()
        {
            var carro1 = new Carro()
            {
                CarroId = 1,
                Marca = "Volkswagen",
                Placa = "ABC-1234"
            };

            var carro2 = new Carro()
            {
                CarroId = 2,
                Marca = "Chevrolet",
                Placa = "XXA-4455"
            };

            return new []{ carro1, carro2 };
        }
    }
}
