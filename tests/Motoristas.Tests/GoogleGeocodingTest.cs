using GoogleGeolocationService;
using Motoristas.Config;
using Motoristas.Core.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Serilog;

namespace Motoristas.Tests
{
    public class GoogleGeocodingTest
    {
        [Fact]
        public async void Can_handle_google_geolocation_request()
        {
            var config =  new GoogleClientConfig("https://maps.googleapis.com/maps/api/geocode/json", "z53osl/q");
            var log = Substitute.For<ILogger>();
            IGeolocationService service = new GeolocationService(config, log);

            var result = service.ObterCoordenadas("Avenida Paulista, 1009");

            Assert.NotNull(result);

        }
    }
}
