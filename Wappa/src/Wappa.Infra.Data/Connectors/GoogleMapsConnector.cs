using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Connectors;
using Wappa.Domain.Interfaces.Models;
using Wappa.Domain.Models;

namespace Wappa.Infra.Data.Connectors
{
    public class GoogleMapsConnector : IGoogleMapsConnector
    {
        private readonly IGoogleMapsConfiguration configuration;

        public GoogleMapsConnector()
        {
        }

        public GoogleMapsConnector(IGoogleMapsConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ILocation> GetLocationAsync(IAddress address)
        {
            var url = WebUtility.UrlEncode($"{address.StreetName},{address.Number},{address.City},{address.StateCode}");
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync($"{configuration.Url}{url}{configuration.Key}");
                var googleMaps = JsonConvert.DeserializeObject<GoogleMapsRoot>(result);

                var hasResult = googleMaps?.Results?.Any() ?? false;

                if (hasResult)
                {
                    var localization = googleMaps.Results.FirstOrDefault().Geometry.Location;
                    return new Location(localization.Latitude, localization.Longtude);
                }

                return new Location(0, 0);
            }
        }
    }
}