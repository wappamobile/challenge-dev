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
using Wappa.Domain.Common;
using Wappa.WebApi.ViewModels.Request;

namespace Wappa.Tests
{
    public class MotoristasUnitTest
    {
        //[Fact]
        //public void GetAllPorNome()
        //{
        //    // Arrange
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock.Setup(repo => repo.GetMotoristaRepository().Listar(OrdenarPor.Nome)).Returns(listarTodosMotoristasSuccess());
        //    var controller = new MotoristasController(unitOfWorkMock.Object);
        //    var request = new GetMotoristasRequest
        //    {
        //        OrdenarPor = "nome"
        //    };

        //    // Act
        //    var model = controller.Get(request);

        //    // Assert
        //    Assert.Equal(2, model.Count);
        //    Assert.IsType<MotoristaViewModel[]>(model.Items);
        //}

        //[Fact]
        //public void GetAllPorSobrenome()
        //{
        //   /* // Arrange
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock.Setup(repo => repo.GetMotoristaRepository().Listar(OrdenarPor.Sobrenome)).Returns(listarTodosMotoristasSuccess());
        //    var controller = new MotoristasController(unitOfWorkMock.Object);
        //    var request = new GetMotoristasRequest
        //    {
        //        OrdenarPor = "sobrenome"
        //    };

        //    // Act
        //    var model = controller.Get(request);

        //    // Assert
        //    Assert.Equal(2, model.Count);
        //    Assert.IsType<MotoristaViewModel[]>(model.Items);*/
        //}

        private Motorista[] listarTodosMotoristasSuccess()
        {
            var motorista1 = new Motorista
            {
                Logradouro = "Rua Testeaaa",
                Bairro = "Teste",
                Carro = new Carro
                {
                    CarroId = 1,
                    Marca = "asdads",
                    Placa = "asdasdd"
                },
                Cidade = new Cidade
                {
                    CidadeId = 1,
                    Nome = "teste",
                    Estado = new Estado
                    {
                        EstadoId = 1,
                        Descricao = "Sao paulo",
                        Sigla = "SP"
                    }
                },
                Latitude = 20.0000,
                Longitude = -22.0000,
                Nome = "AAA",
                Numero = "2233",
                Sobrenome = "bbbb"
            };

            var motorista2 = new Motorista
            {
                Logradouro = "Rua AAAA",
                Bairro = "BBBBBB",
                Carro = new Carro
                {
                    CarroId = 2,
                    Marca = "BBBBB",
                    Placa = "BBBB"
                },
                Cidade = new Cidade
                {
                    CidadeId = 1,
                    Nome = "teste",
                    Estado = new Estado
                    {
                        EstadoId = 1,
                        Descricao = "Sao paulo",
                        Sigla = "SP"
                    }
                },
                Latitude = -20.0000,
                Longitude = -22.0000,
                Nome = "BBBB",
                Numero = "BBBB",
                Sobrenome = "BBBBBBB"
            };


            return new[] { motorista1, motorista2 };
        }
    }
}
