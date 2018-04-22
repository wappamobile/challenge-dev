using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Business.Implementations;
using Wappa.Business.Interfaces;
using Wappa.DataAccess.Interfaces;
using Wappa.Models;
using Wappa.Models.Enum;
using Xunit;

namespace Wappa.Test
{
    public class MotoristaBusinessTest
    {
        [Fact]
        public void ObterMotoristaPorIdValido()
        {
            //Arrange
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.ObterPorId(1)).Returns(() => new Motorista());

            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();

            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            var result = motoristaBusiness.ObterPorId(1);

            //Assert
            result.Should().NotBeNull("Resultado incorreto para um id válido");
        }

        [Fact]
        public async void IncluirMotorista()
        {
            //Arrange
            var motorista = new Motorista { Endereco = new Endereco() };
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.Incluir(motorista)).Callback(() => motorista.Id = 1);

            var localizacao = new Localizacao { Latitude = -23.5624692, Longitude = -46.6389619 };
            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();
            mockLocalizacaoBusiness.Setup(x => x.ObterCoordenadas(motorista.Endereco)).Returns(() => Task.FromResult(localizacao));

            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            await motoristaBusiness.Incluir(motorista);

            //Assert
            motorista.Id.Should().Be(1, "Resultado incorreto ao incluir motorista");
        }

        [Fact]
        public async void AlterarMotorista()
        {
            //Arrange
            var motorista = new Motorista { Endereco = new Endereco() };
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.Alterar(motorista)).Returns(() => true);

            var localizacao = new Localizacao { Latitude = -23.5624692, Longitude = -46.6389619 };
            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();
            mockLocalizacaoBusiness.Setup(x => x.ObterCoordenadas(motorista.Endereco)).Returns(() => Task.FromResult(localizacao));

            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            var result = await motoristaBusiness.Alterar(motorista);

            //Assert
            result.Should().BeTrue("Resultado incorreto ao alterar motorista");
        }

        [Fact]
        public void ExcluirMotorista()
        {
            //Arrange
            var motorista = new Motorista { Endereco = new Endereco() };
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.Excluir(1)).Returns(() => true);

            var localizacao = new Localizacao { Latitude = -23.5624692, Longitude = -46.6389619 };
            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();
            mockLocalizacaoBusiness.Setup(x => x.ObterCoordenadas(motorista.Endereco)).Returns(() => Task.FromResult(localizacao));

            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            var result = motoristaBusiness.Excluir(1);

            //Assert
            result.Should().BeTrue("Resultado incorreto ao excluir motorista");
        }

        [Fact]
        public void ObterMotoristaPorIdInvalido()
        {
            //Arrange
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.ObterPorId(1)).Returns(() => null);

            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();

            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            var result = motoristaBusiness.ObterPorId(1);

            //Assert
            result.Should().BeNull("Resultado incorreto para um id inválido");
        }

        [Fact]
        public void ListarMotoristas()
        {
            //Arrange
            var motoristas = CriarListaDeMotoristas();
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.ListarTodos()).Returns(() => motoristas);

            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();
            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            var result = motoristaBusiness.Listar(CampoOrdenacaoEnum.Nenhum);

            //Assert
            result.Should().NotBeNullOrEmpty("Resultado incorreto para listar motoristas");
        }

        [Fact]
        public void ListarMotoristasOrdenadosPorNome()
        {
            //Arrange
            var motoristas = CriarListaDeMotoristas();
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.ListarTodos()).Returns(() => motoristas);

            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();
            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            var result = motoristaBusiness.Listar(CampoOrdenacaoEnum.Nome);

            var estaOrdenado = motoristas.OrderBy(m => m.Nome).SequenceEqual(result);

            //Assert
            estaOrdenado.Should().BeTrue("Resultado incorreto ao listar motoristas ordenados pelo nome");
        }

        [Fact]
        public void ListarMotoristasOrdenadosPorUltimoNome()
        {
            //Arrange
            var motoristas = CriarListaDeMotoristas();
            var mockMotoristaRepository = new Mock<IMotoristaRepository>();
            mockMotoristaRepository.Setup(x => x.ListarTodos()).Returns(() => motoristas);

            var mockLocalizacaoBusiness = new Mock<ILocalizacaoBusiness>();
            var motoristaBusiness = new MotoristaBusiness(mockLocalizacaoBusiness.Object, mockMotoristaRepository.Object);

            //Act
            var result = motoristaBusiness.Listar(CampoOrdenacaoEnum.UltimoNome);

            var estaOrdenado = motoristas.OrderBy(m => m.UltimoNome).SequenceEqual(result);

            //Assert
            estaOrdenado.Should().BeTrue("Resultado incorreto ao listar motoristas ordenado pelo último nome");
        }

        private List<Motorista> CriarListaDeMotoristas()
        {
            var motoristas = new List<Motorista>();
            motoristas.Add(new Motorista
            {
                Carro = new Carro
                {
                    Id = 1,
                    Marca = "Chevrolet",
                    Modelo = "Prisma",
                    Placa = "ABR-2104"
                },
                Endereco = new Endereco
                {
                    CEP = "12345-000",
                    Cidade = "São Paulo",
                    Id = 1,
                    Latitude = "",
                    Longitude = "",
                    Numero = 88,
                    Rua = "Rua Vergueiro",
                    UF = "SP"

                },
                Id = 1,
                Nome = "Carlos",
                UltimoNome = "Alberto Souza"
            });
            motoristas.Add(new Motorista
            {
                Carro = new Carro
                {
                    Id = 2,
                    Marca = "Chevrolet",
                    Modelo = "Prisma",
                    Placa = "ABR-2104"
                },
                Endereco = new Endereco
                {
                    Id = 2,
                    CEP = "12345-000",
                    Cidade = "São Paulo",
                    Latitude = "",
                    Longitude = "",
                    Numero = 88,
                    Rua = "Rua Vergueiro",
                    UF = "SP"

                },
                Id = 2,
                Nome = "João",
                UltimoNome = "Silva"
            });
            motoristas.Add(new Motorista
            {
                Carro = new Carro
                {
                    Id = 2,
                    Marca = "Chevrolet",
                    Modelo = "Prisma",
                    Placa = "ABR-2104"
                },
                Endereco = new Endereco
                {
                    Id = 2,
                    CEP = "12345-000",
                    Cidade = "São Paulo",
                    Latitude = "",
                    Longitude = "",
                    Numero = 88,
                    Rua = "Rua Vergueiro",
                    UF = "SP"

                },
                Id = 2,
                Nome = "Paulo",
                UltimoNome = "Cruz"
            });

            return motoristas;
        }
    }
}
