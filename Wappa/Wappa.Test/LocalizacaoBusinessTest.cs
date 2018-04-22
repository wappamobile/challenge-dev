using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Business.Implementations;
using Wappa.DataAccess.Interfaces;
using Wappa.Models;
using Xunit;

namespace Wappa.Test
{
    public class LocalizacaoBusinessTest
    {
        [Fact]
        public void ObterCoordenadas()
        {
            //Arrange
            var endereco = new Endereco();
            var mockGoogleMapsClient = new Mock<IGoogleMapsClient>();
            mockGoogleMapsClient.Setup(x => x.ObterCoordenadas(endereco)).Returns(() => Task.FromResult(new Localizacao { Latitude = -23.5624692, Longitude = -46.6389619 }));

            var localizacaoBusiness = new LocalizacaoBusiness(mockGoogleMapsClient.Object);

            //Act
            var result = localizacaoBusiness.ObterCoordenadas(endereco);

            //Assert
            result.Result.Should().NotBeNull("Resultado incorreto ao buscar coordenadas");
        }
    }
}
