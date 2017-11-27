using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;
using WappaChallenge.Repositorio.Databases;
using WappaChallenge.Repositorio.Repositorios;

namespace TDD.TestsVeiculo
{
    [TestClass]
    public class RepositorioVeiculoTest
    {
        private IVeiculoRepositorio _repositorio;

        [TestInitialize]
        public void TestInitialize()
        {
            this._repositorio = new VeiculoRepositorio(new MockStaticDatabase());

            for (int i = 0; i < 10; i++)
            {
                this._repositorio.Cadastrar(new Veiculo(
                            marca: "Chevrolet",
                            modelo: "Ônix LT 1.0 2017",
                            placa: "QIV-2872"));
            }
        }

        [TestMethod]
        public void DeveSerPossivelCadastrarUmVeiculo()
        {
            Veiculo veiculo = new Veiculo(
                 marca: "Chevrolet",
                 modelo: "Ônix LT 1.0 2017",
                 placa: "QIV-2872");

            veiculo = this._repositorio.Cadastrar(veiculo);
            Assert.AreEqual(veiculo.Id, this._repositorio.BuscarPorId(veiculo.Id).Id);
        }

        [TestMethod]
        public void DeveSerPossivelAtualizarUmEndereco()
        {
            Veiculo veiculo = new Veiculo(
                marca: "Chevrolet",
                modelo: "Ônix LT 1.0 2017",
                placa: "QIV-2872");

            veiculo.Id = 4;

            veiculo = this._repositorio.Atualizar(veiculo);
            Veiculo veiculoDb = this._repositorio.BuscarPorId(4);

            Assert.AreEqual(veiculoDb.Id, veiculo.Id);
            Assert.AreEqual(veiculoDb.Marca, veiculo.Marca);
            Assert.AreEqual(veiculoDb.Modelo, veiculo.Modelo);
            Assert.AreEqual(veiculoDb.Placa, veiculo.Placa);
        }

        [TestMethod]
        public void DeveSerPossivelExcluirUmEndereco()
        {
            int veiculoId = 4;

            this._repositorio.Excluir(veiculoId);
            Veiculo veiculoDb = this._repositorio.BuscarPorId(4);

            Assert.IsNull(veiculoDb);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._repositorio = null;
        }
    }
}
