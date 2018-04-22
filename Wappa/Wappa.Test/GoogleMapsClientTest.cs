using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.DataAccess.Implementations;
using Wappa.Models;
using Xunit;

namespace Wappa.Test
{
    public class GoogleMapsClientTest
    {
        [Fact]
        public async void ObterCoordenadasGoogleMapsAPI()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();
            //Arrange
            var endereco = new Endereco
            {
                Rua = "Rua Vergueiro",
                Numero = 88,
                Cidade = "São Paulo",
                UF = "SP"
            };
            var googleMapsClient = new GoogleMapsClient(config);

            //Act
            var result = await googleMapsClient.ObterCoordenadas(endereco);

            //Assert
            result.Should().NotBeNull("Resultado incorreto ao buscar coordenadas no API do google maps");
        }
    }
}
