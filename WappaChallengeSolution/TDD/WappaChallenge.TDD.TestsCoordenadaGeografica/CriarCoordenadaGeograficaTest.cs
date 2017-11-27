using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.Dominio.Entidades;

namespace WappaChallenge.TDD.TestsCoordenadaGeografica
{
    [TestClass]
    public class CriarCoordenadaGeograficaTest
    {
        [TestMethod]
        public void DeveSerPossivelCriarUmCoordenadaGeografica()
        {
            CoordenadaGeografica coordenada = new CoordenadaGeografica(
                latitude: -23.564236F,
                longitude: -46.839648F);

            Assert.IsNotNull(coordenada);
        }
    }
}
