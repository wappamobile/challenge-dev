using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.API.Controllers;
using Wappa.API.ViewModel;
using Wappa.Business.Interfaces;
using Wappa.Models;
using Wappa.Models.Enum;
using Xunit;

namespace Wappa.Test
{
    public class MotoristaControllerTest
    {
        [Fact]
        public void ObterMotoristaPorIdValido()
        {
            //Arrange
            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.ObterPorId(1)).Returns(() => new Motorista());

            var mapperMock = new Mock<IMapper>();

            var motoristaController = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object);

            //Act
            var result = motoristaController.ObterPorId(1);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.StatusCode.Should().Be(200, "Resultado incorreto para um id válido");
        }

        [Fact]
        public void ObterMotoristaPorIdInvalido()
        {
            //Arrange
            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.ObterPorId(1)).Returns(() => null);

            var mapperMock = new Mock<IMapper>();

            var motoristaController = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object);

            //Act
            var result = motoristaController.ObterPorId(1);

            var notFoundResult = result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404, "Resultado incorreto para um id inválido");
        }

        [Fact]
        public void ListarMotoristas()
        {
            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Listar(CampoOrdenacaoEnum.Nenhum)).Returns(() => new List<Motorista>());

            var mapperMock = new Mock<IMapper>();
            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).ListarOrdenadoPorNome() as OkObjectResult;

            result.StatusCode.Should().Be(200, "Resultado incorreto ao listar motoristas");
        }

        [Fact]
        public void ListarMotoristasOrdenadoPorNome()
        {
            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Listar(CampoOrdenacaoEnum.Nome)).Returns(() => new List<Motorista>());

            var mapperMock = new Mock<IMapper>();

            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).ListarOrdenadoPorNome() as OkObjectResult;

            result.StatusCode.Should().Be(200, "Resultado incorreto ao listar motoristas ordernados por nome");
        }

        [Fact]
        public void ListarMotoristasOrdenadoPorUltimoNome()
        {
            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Listar(CampoOrdenacaoEnum.UltimoNome)).Returns(() => new List<Motorista>());

            var mapperMock = new Mock<IMapper>();
            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).ListarOrdenadoPorUltimoNome() as OkObjectResult;
            result.StatusCode.Should().Be(200, "Resultado incorreto ao listar motoristas ordenados por último nome");
        }

        [Fact]
        public void IncluirMotorista()
        {
            var motoristaViewModelNovo = new MotoristaViewModel();
            var motoristaNovo = new Motorista();

            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Incluir(motoristaNovo)).Returns(() => Task.FromResult(motoristaNovo));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MotoristaViewModel, Motorista>(motoristaViewModelNovo)).Returns(() => motoristaNovo);

            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).Incluir(motoristaViewModelNovo);
            result.Wait();

            var createdResult = result.Result as CreatedResult;

            createdResult.StatusCode.Should().Be(201, "Resultado incorreto ao incluir motorista");
        }

        [Fact]
        public void IncluirMotoristaSemCamposObrigatorios()
        {
            var motoristaViewModelNovo = new MotoristaViewModel();
            var motoristaNovo = new Motorista();

            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Incluir(motoristaNovo)).Returns(() => Task.FromResult(motoristaNovo));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MotoristaViewModel, Motorista>(motoristaViewModelNovo)).Returns(() => motoristaNovo);

            var motoristaController = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object);
            motoristaController.ModelState.AddModelError("error", "error");

            var result = motoristaController.Incluir(motoristaViewModelNovo);
            result.Wait();

            var badRequestResult = result.Result as BadRequestObjectResult;

            badRequestResult.StatusCode.Should().Be(400, "Resultado incorreto ao incluir motorista com model inválida");
        }

        [Fact]
        public void AlterarMotorista()
        {
            var motoristaViewModelNovo = new MotoristaViewModel();
            var motoristaNovo = new Motorista();

            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Alterar(motoristaNovo)).Returns(() => Task.FromResult(true));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MotoristaViewModel, Motorista>(motoristaViewModelNovo)).Returns(() => motoristaNovo);

            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).Alterar(motoristaViewModelNovo);
            result.Wait();

            var createdResult = result.Result as OkObjectResult;
            createdResult.StatusCode.Should().Be(200, "Resultado incorreto ao alterar motorista");
        }

        [Fact]
        public void AlterarMotoristaQueNaoExiste()
        {
            var motoristaViewModelNovo = new MotoristaViewModel();
            var motoristaNovo = new Motorista();

            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Alterar(motoristaNovo)).Returns(() => Task.FromResult(false));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MotoristaViewModel, Motorista>(motoristaViewModelNovo)).Returns(() => motoristaNovo);

            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).Alterar(motoristaViewModelNovo);
            result.Wait();

            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404, "Resultado incorreto ao alterar motorista que não existe");
        }

        [Fact]
        public void AlterarMotoristaSemCamposObrigatorios()
        {
            var motoristaViewModelNovo = new MotoristaViewModel();
            var motoristaNovo = new Motorista();

            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Alterar(motoristaNovo)).Returns(() => Task.FromResult(false));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MotoristaViewModel, Motorista>(motoristaViewModelNovo)).Returns(() => motoristaNovo);

            var motoristaController = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object);
            motoristaController.ModelState.AddModelError("error", "error");

            var result = motoristaController.Alterar(motoristaViewModelNovo);
            result.Wait();

            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.StatusCode.Should().Be(400, "Resultado incorreto ao alterar motorista com model inválida");
        }

        [Fact]
        public void ExcluirMotorista()
        {
            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Excluir(1)).Returns(() => true);

            var mapperMock = new Mock<IMapper>();

            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).Excluir(1);

            var createdResult = result as OkResult;
            createdResult.StatusCode.Should().Be(200, "Resultado incorreto ao excluir motorista");
        }

        [Fact]
        public void ExcluirMotoristaQueNaoExiste()
        {
            var mockMotoristaBusiness = new Mock<IMotoristaBusiness>();
            mockMotoristaBusiness.Setup(x => x.Excluir(1)).Returns(() => false);

            var mapperMock = new Mock<IMapper>();

            var result = new MotoristaController(mockMotoristaBusiness.Object, mapperMock.Object).Excluir(1);

            var notFoundResult = result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404, "Resultado incorreto ao excluir motorista que não existe");
        }
    }
}
