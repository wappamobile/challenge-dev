using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Wappa.Domain.Entidades;
using Wappa.Domain.Services;

namespace Wappa.DomainTest
{
    [TestFixture]
    public class MotoristaServiceTest
    {
        private MockRepository mockRepository;

        [SetUp]
        public void Initialize()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Test]
        public void InserirMotorista_Valido()
        {
            var motoristaGateway = mockRepository.Create<IMotoristaGateway>();
            var localizacaoGateway = mockRepository.Create<ILocalizacaoGateway>();

            var motorista = new Motorista()
            {
                MotoristaID = 0,
                PrimeiroNome = "Teste",
                UltimoNome = "Teste",
                EnderecoID = 0,
                VeiculoID = 0,
                Endereco = new Endereco()
                {
                    EnderecoID = 0,
                    Logradouro = "Teste",
                    Numero = "1",
                    Bairro = "Teste",
                    Cidade = "Teste",
                    Estado = "TT"
                },
                Veiculo = new Veiculo()
                {
                    VeiculoID = 0,
                    Marca = "TT",
                    Modelo = "Teste",
                    Placa = "TTT-1111"
                }
            };

            var coord = new Dictionary<string, string>();
            coord.Add("lat", "12.12");
            coord.Add("lng", "12.12");

            motoristaGateway.Setup(m => m.Novo(motorista));
            localizacaoGateway.Setup(m => m.ObterCoordenadas(It.IsAny<string>())).Returns(coord);

            var service = new MotoristaService(motoristaGateway.Object, localizacaoGateway.Object);

            service.Novo(motorista);

            motoristaGateway.Verify(m => m.Novo(motorista));
        }

        [Test]
        public void ApagarMotorista()
        {
            var motoristaGateway = mockRepository.Create<IMotoristaGateway>();
            var localizacaoGateway = mockRepository.Create<ILocalizacaoGateway>();

            motoristaGateway.Setup(p => p.Excluir(1));

            var service = new MotoristaService(motoristaGateway.Object, localizacaoGateway.Object);

            service.Excluir(1);

            motoristaGateway.Verify(m => m.Excluir(1));
        }
    }
}
