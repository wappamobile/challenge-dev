using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.Repositorio.Databases;
using WappaChallenge.Repositorio.Repositorios;

namespace TDD.TestsCoordenadaGeografica
{
    [TestClass]
    public class RepositorioCoordenadaGeograficaTest
    {
        private ICoordenadaGeograficaRepositorio _repositorio;

        [TestInitialize]
        public void TestInitialize()
        {
            this._repositorio = new CoordenadaGeograficaRepositorio(new MockStaticDatabase());

            _repositorio.Cadastrar(new CoordenadaGeografica(-1, -2));
            _repositorio.Cadastrar(new CoordenadaGeografica(-1, -2));
            _repositorio.Cadastrar(new CoordenadaGeografica(-1, -2));
            _repositorio.Cadastrar(new CoordenadaGeografica(-1, -2));
        }

        [TestMethod]
        public void DeveSerPossivelCadastrarUmaCoordenadaGeografica()
        {
            CoordenadaGeografica coordenada = new CoordenadaGeografica(
                latitude: -23.564236F,
                longitude: -46.839648F);

            coordenada = this._repositorio.Cadastrar(coordenada);

            Assert.AreEqual(coordenada.Id, this._repositorio.BuscarPorId(coordenada.Id).Id);
        }

        [TestMethod]
        public void DeveSerPossivelAtualizarUmaCoordenadaGeografica()
        {
            CoordenadaGeografica coordenada = new CoordenadaGeografica(
               latitude: -23.56423600F,
               longitude: -46.83964800F);

            coordenada.Id = 4;

            coordenada = this._repositorio.Atualizar(coordenada);
            CoordenadaGeografica coordenadaDb = this._repositorio.BuscarPorId(4);

            Assert.AreEqual(coordenadaDb.Id, coordenada.Id);
            Assert.AreEqual(coordenadaDb.Latitude, coordenada.Latitude);
            Assert.AreEqual(coordenadaDb.Longitude, coordenada.Longitude);
        }

        [TestMethod]
        public void DeveSerPossivelExcluirUmaCoordenadaGeografica()
        {
            int coordenadaId = 4;

            this._repositorio.Excluir(coordenadaId);
            CoordenadaGeografica coordenadaDb = this._repositorio.BuscarPorId(4);

            Assert.IsNull(coordenadaDb);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._repositorio = null;
        }
    }
}
