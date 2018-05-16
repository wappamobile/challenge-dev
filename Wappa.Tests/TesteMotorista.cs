using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Wappa;
using Xunit;

namespace Wappa.Tests {
    public class TesteMotorista {

        private Wappa.Services.V1.MotoristaService motoristaService { get; set; }
        private Wappa.Services.V1.GeolocalizacaoService geoService { get; set; }

        public TesteMotorista () {
            motoristaService = new Wappa.Services.V1.MotoristaService ();
        }

        [Fact]
        public void TesteObterMotoristas () {

            var result = motoristaService.ObterMotoristas (false);

            Assert.Equal (result.Count, 3);
        }

    }
}