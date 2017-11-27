using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.Repositorio.Databases;
using WappaChallenge.Repositorio.Repositorios;

namespace TDD.TestsEndereco
{
    [TestClass]
    public class RepositorioEnderecoTest
    {
        private IEnderecoRepositorio _repositorio;

        [TestInitialize]
        public void TestInitialize()
        {
            this._repositorio = new EnderecoRepositorio(new MockStaticDatabase());

            for (int i = 0; i < 10; i++)
            {
                this._repositorio.Cadastrar(new Endereco(
                                logradouro: "Avenida Paraguaçu Paulista",
                                numero: "287 B",
                                complemento: "Casa 3",
                                bairro: "Jardim Ana Estela",
                                cidade: "Carapicuíba",
                                estado: "São Paulo",
                                cep: "06364-550",
                                coordenadas: new CoordenadaGeografica(-23.564236F, -46.839648F)));
            }
        }

        [TestMethod]
        public void DeveSerPossivelCadastrarUmEndereco()
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

            endereco = this._repositorio.Cadastrar(endereco);
            Assert.AreEqual(endereco.Id, this._repositorio.BuscarPorId(endereco.Id).Id);
        }

        [TestMethod]
        public void DeveSerPossivelAtualizarUmEndereco()
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

            endereco.Id = 4;

            endereco = this._repositorio.Atualizar(endereco);
            Endereco enderecoDb = this._repositorio.BuscarPorId(4);

            Assert.AreEqual(enderecoDb.Id, endereco.Id);
            Assert.AreEqual(enderecoDb.Logradouro, endereco.Logradouro);
            Assert.AreEqual(enderecoDb.Numero, endereco.Numero);
            Assert.AreEqual(enderecoDb.Complemento, endereco.Complemento);
            Assert.AreEqual(enderecoDb.Bairro, endereco.Bairro);
            Assert.AreEqual(enderecoDb.Cidade, endereco.Cidade);
            Assert.AreEqual(enderecoDb.Estado, endereco.Estado);
            Assert.AreEqual(enderecoDb.CEP, endereco.CEP);
            Assert.AreEqual(enderecoDb.CoordenadaGeografica.Latitude, endereco.CoordenadaGeografica.Latitude);
            Assert.AreEqual(enderecoDb.CoordenadaGeografica.Longitude, endereco.CoordenadaGeografica.Longitude);
        }

        [TestMethod]
        public void DeveSerPossivelExcluirUmEndereco()
        {
            int enderecoId = 4;

            this._repositorio.Excluir(enderecoId);
            Endereco enderecoDb = this._repositorio.BuscarPorId(4);

            Assert.IsNull(enderecoDb);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._repositorio = null;
        }
    }
}
