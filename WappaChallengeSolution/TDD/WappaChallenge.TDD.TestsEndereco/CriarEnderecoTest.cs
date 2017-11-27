using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Exceptions;

namespace WappaChallenge.TDD.TestsCoordenadaGeografica
{
    [TestClass]
    public class CriarEnderecoTest
    {
        [TestMethod]
        public void DeveSerPossivelCriarUmEndereco()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: "Casa 3",
                bairro: "Jardim Ana Estela",
                cidade: "Carapicuíba",
                estado: "São Paulo",
                cep: "06364-550",
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));

            Assert.IsNotNull(endereco);
        }

        [TestMethod]
        public void DeveSerPossivelCriarUmEnderecoSemComplemento()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: string.Empty,
                bairro: "Jardim Ana Estela",
                cidade: "Carapicuíba",
                estado: "São Paulo",
                cep: "06364-550",
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));

            Assert.IsNotNull(endereco);
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoSemLogradouro()
        {
            Endereco endereco = new Endereco(
                 logradouro: string.Empty,
                 numero: "287 B",
                 complemento: "Casa 3",
                 bairro: "Jardim Ana Estela",
                 cidade: "Carapicuíba",
                 estado: "São Paulo",
                 cep: "06364-550",
                 coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoSemNumero()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: string.Empty,
                complemento: "Casa 3",
                bairro: "Jardim Ana Estela",
                cidade: "Carapicuíba",
                estado: "São Paulo",
                cep: "06364-550",
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoSemBairro()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: "Casa 3",
                bairro: string.Empty,
                cidade: "Carapicuíba",
                estado: "São Paulo",
                cep: "06364-550",
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoSemCidade()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: "Casa 3",
                bairro: "Jardim Ana Estela",
                cidade: string.Empty,
                estado: "São Paulo",
                cep: "06364-550",
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoSemEstado()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: "Casa 3",
                bairro: "Jardim Ana Estela",
                cidade: "Carapicuíba",
                estado: string.Empty,
                cep: "06364-550",
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoSemCEP()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: "Casa 3",
                bairro: "Jardim Ana Estela",
                cidade: "Carapicuíba",
                estado: "São Paulo",
                cep: string.Empty,
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoComCEPInvalido()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: "Casa 3",
                bairro: "Jardim Ana Estela",
                cidade: "Carapicuíba",
                estado: "São Paulo",
                cep: "ABCD-1234",
                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F));
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmEnderecoSemCoordenadasGeograficas()
        {
            Endereco endereco = new Endereco(
                logradouro: "Avenida Paraguaçu Paulista",
                numero: "287 B",
                complemento: "Casa 3",
                bairro: "Jardim Ana Estela",
                cidade: "Carapicuíba",
                estado: "São Paulo",
                cep: "06364-550",
                coordenadas: null);
        }
    }
}