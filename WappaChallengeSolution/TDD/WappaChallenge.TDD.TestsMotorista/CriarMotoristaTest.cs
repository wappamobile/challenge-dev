using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Exceptions;
using WappaChallenge.Dominio.Entidades;

namespace TDD.TestsMotorista
{
    [TestClass]
    public class CriarMotoristaTest
    {

        [TestMethod]
        public void DeveSerPossivelCriarUmMotorista()
        {
            Motorista motorista = new Motorista(
                primeiroNome: "Andrey",
                ultimoNome: "Frazatti",
                veiculo: new Veiculo(
                    marca: "Chevrolet",
                    modelo: "Ônix LT 1.0 2017",
                    placa: "QIV-2872"),
                endereco: new Endereco(
                    logradouro: "Avenida Paraguaçu Paulista",
                    numero: "287 B",
                    complemento: "Casa 3",
                    bairro: "Jardim Ana Estela",
                    cidade: "Carapicuíba",
                    estado: "São Paulo",
                    cep: "06364-550",
                    coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F)));

            Assert.IsNotNull(motorista);
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmMotoristaSemOPrimeiroNome()
        {
            Motorista motorista = new Motorista(
                 primeiroNome: string.Empty,
                 ultimoNome: "Frazatti",
                 veiculo: new Veiculo(
                     marca: "Chevrolet",
                     modelo: "Ônix LT 1.0 2017",
                     placa: "QIV-2872"),
                 endereco: new Endereco(
                     logradouro: "Avenida Paraguaçu Paulista",
                     numero: "287 B",
                     complemento: "Casa 3",
                     bairro: "Jardim Ana Estela",
                     cidade: "Carapicuíba",
                     estado: "São Paulo",
                     cep: "06364-550",
                     coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F)));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmMotoristaSemOUltimoNome()
        {
            Motorista motorista = new Motorista(
                primeiroNome: "Andrey",
                ultimoNome: string.Empty,
                veiculo: new Veiculo(
                    marca: "Chevrolet",
                    modelo: "Ônix LT 1.0 2017",
                    placa: "QIV-2872"),
                endereco: new Endereco(
                    logradouro: "Avenida Paraguaçu Paulista",
                    numero: "287 B",
                    complemento: "Casa 3",
                    bairro: "Jardim Ana Estela",
                    cidade: "Carapicuíba",
                    estado: "São Paulo",
                    cep: "06364-550",
                    coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F)));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmMotoristaSemVeiculo()
        {
            Motorista motorista = new Motorista(
                primeiroNome: "Andrey",
                ultimoNome: "Frazatti",
                veiculo: null,
                endereco: new Endereco(
                    logradouro: "Avenida Paraguaçu Paulista",
                    numero: "287 B",
                    complemento: "Casa 3",
                    bairro: "Jardim Ana Estela",
                    cidade: "Carapicuíba",
                    estado: "São Paulo",
                    cep: "06364-550",
                    coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F)));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmMotoristaSemEndereco()
        {
            Motorista motorista = new Motorista(
                primeiroNome: "Andrey",
                ultimoNome: "Frazatti",
                veiculo: new Veiculo(
                    marca: "Chevrolet",
                    modelo: "Ônix LT 1.0 2017",
                    placa: "QIV-2872"),
                endereco: null);
        }
    }
}
