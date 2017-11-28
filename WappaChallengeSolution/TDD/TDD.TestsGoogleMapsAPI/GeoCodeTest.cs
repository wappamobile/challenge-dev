using Microsoft.VisualStudio.TestTools.UnitTesting;
using WappaChallenge.AppServices.Facade;

namespace TDD.TestsGoogleMapsAPI
{
    [TestClass]
    public class GeoCodeTest
    {
        [TestMethod]
        public void DeveSerPossivelFazerUmaRequisicaoGETNoGoogle()
        {
            GoogleMapsAPIFacade facade = new GoogleMapsAPIFacade();
            var resultado = facade.ObterCoordenadasGeograficas(new WappaChallenge.DTO.EnderecoDTO
            {
                Logradouro = "Avenida Paragua�u Paulista",
                Numero = "287 B",
                Complemento = "Casa 3",
                Bairro = "Jardim Ana Estela",
                Cidade = "Carapicu�ba",
                Estado = "S�o Paulo",
                CEP = "06364-550"
            }).Result;

            Assert.IsNotNull(resultado);
        }
    }
}
