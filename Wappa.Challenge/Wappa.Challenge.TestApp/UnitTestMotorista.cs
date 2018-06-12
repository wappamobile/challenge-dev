using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;
using Wappa.Challenge.ApplicationCore.Services;
using Wappa.Challenge.Infrastructure.Repositories;

namespace Wappa.Challenge.TestApp
{
    [TestFixture]
    public class UnitTestMotorista
    {
        private Mock<IMotoristaRepository> _mockMotorista;

        private MotoristaService GetMotorista()
        {
            _mockMotorista = new Mock<IMotoristaRepository>();
            var motora = new MotoristaService(_mockMotorista.Object);
            return motora;
        }

        [Test]
        public void NovoMotora()
        {
            var hora = DateTime.Now;

            var novoMotorista = new Motorista()
            {
                Nome = $"Leo {hora}",
                Sobrenome = $"Lima {hora}",
                Carro = new List<Carro>()
                {
                    new Carro()
                    {
                        Marca = $"Hyunday {hora}",
                        Modelo = "HB20",
                        Placa = "EJH-3444"
                    }
                },
                Endereco = new List<Endereco>()
                {
                    new Endereco()
                    {
                        Logradouro = "Av. João Firmino",
                        Numero = "S/N",
                        Bairro = "Assunção",
                        Cidade = "São Bernardo",
                        Estado = "SP",
                        Cep = "09810-260",
                        Complemento = $"ap 82 {hora}",
                    }
                },
                DataInclusao = DateTime.Now
            };

            MotoristaService motora = GetMotorista();
            var resultado = motora.Adicionar(novoMotorista);
            _mockMotorista.Verify(m => m.Adicionar(It.IsAny<Motorista>()), Times.Once);
        }

        [Test]
        public void ApagarMotora()
        {
            MotoristaService motora = GetMotorista();
            var resultado = motora.Apagar(1);
            _mockMotorista.Verify(m => m.Apagar(It.IsAny<int>()), Times.Once);
        }
    }
}
