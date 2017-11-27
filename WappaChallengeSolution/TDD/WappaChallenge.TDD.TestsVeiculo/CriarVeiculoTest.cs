using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Exceptions;

namespace WappaChallenge.TDD.TestsCoordenadaGeografica
{
    [TestClass]
    public class CriarVeiculoTest
    {
        [TestMethod]
        public void DeveSerPossivelCriarUmVeiculo()
        {
            Veiculo veiculo = new Veiculo(
                marca: "Chevrolet", 
                modelo: "Ônix LT 1.0 2017", 
                placa: "QIV-2872");

            Assert.IsNotNull(veiculo);
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmVeiculoSemMarca()
        {
            Veiculo veiculo = new Veiculo(
                marca: string.Empty,
                modelo: "Ônix LT 1.0 2017",
                placa: "QIV-2872");
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmVeiculoSemModelo()
        {
            Veiculo veiculo = new Veiculo(
                marca: "Chevrolet",
                modelo: string.Empty,
                placa: "QIV-2872");
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmVeiculoSemPlaca()
        {
            Veiculo veiculo = new Veiculo(
                marca: "Chevrolet",
                modelo: "Ônix LT 1.0 2017",
                placa: string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void NaoDeveSerPossivelCriarUmVeiculoComPlavaInvalida()
        {
            Veiculo veiculo = new Veiculo(
                marca: "Chevrolet",
                modelo: "Ônix LT 1.0 2017",
                placa: "ABCD-8800");
        }
    }
}
