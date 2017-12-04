using System;
using Wappa.Domain.UnitOfWork;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Wappa.Domain.Entities;
using System.Collections.Generic;
using Wappa.WebApi.Controllers;
using System.Linq;
using Wappa.WebApi.ViewModels.Common;

namespace Wappa.Tests
{
    public class CidadesUnitTest
    {
        [Fact]
        public void GetAll()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(repo => repo.GetCidadeRepository().ListarTodos()).Returns(listarTodasCidadesSuccess());
            var controller = new CidadesController(unitOfWorkMock.Object);

            // Act
            var model = controller.Get();

            // Assert
            Assert.Equal(2, model.Count);
            Assert.IsType<CidadeViewModel[]>(model.Items);
        }

        private Cidade[] listarTodasCidadesSuccess()
        {
            var cidade1 = new Cidade()
            {
                CidadeId = 1,
                Nome = "Barueri",
                Estado = new Estado
                {
                    EstadoId = 1,
                    Descricao = "São Paulo",
                    Sigla = "SP"
                }
            };

            var cidade2 = new Cidade()
            {
                CidadeId = 2,
                Nome = "Santos",
                Estado = new Estado
                {
                    EstadoId = 1,
                    Descricao = "São Paulo",
                    Sigla = "SP"
                }
            };

            return new []{ cidade1, cidade2 };
        }
    }
}
