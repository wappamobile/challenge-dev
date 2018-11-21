using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Connectors;
using Wappa.Infra.Data.Connectors;
using Wappa.Test.Wappa.Domain;
using Xunit;

namespace Wappa.Test.Wappa.Infra.Data.Connectors
{
    public class GoogleMapsConnectorTest
    {
        private readonly IGoogleMapsConfiguration _configuration;

        public GoogleMapsConnectorTest()
        {
            _configuration = GoogleMapsHelper.GoogleMapsConfiguration();
        }

        [Fact]
        public async Task Should_CallGoogleMapsApiOKAsync()
        {
            var connector = new GoogleMapsConnector(_configuration);

            var location = await connector.GetLocationAsync(AddressFake.GetAddress());

            Assert.NotNull(location);
            Assert.True(location.Latitude != 0);
            Assert.True(location.Longitude != 0);
        }
    }
}