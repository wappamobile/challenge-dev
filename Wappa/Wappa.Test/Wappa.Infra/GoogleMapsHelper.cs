using NSubstitute;
using Wappa.Domain.Interfaces.Connectors;

namespace Wappa.Test.Wappa.Infra
{
    public static class GoogleMapsHelper
    {
        private const string Url = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        private const string Key = "&key=AIzaSyDyYF-3VZh0bkpEAcRQt-yCF6Wc_J0t6Ew";

        public static IGoogleMapsConfiguration GoogleMapsConfiguration()
        {
            var _configuration = Substitute.For<IGoogleMapsConfiguration>();
            _configuration.Url.Returns(Url);
            _configuration.Key.Returns(Key);

            return _configuration;
        }
    }
}