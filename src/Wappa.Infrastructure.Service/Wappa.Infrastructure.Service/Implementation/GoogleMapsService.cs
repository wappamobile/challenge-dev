using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using Wappa.Infrastructure.Service.Interface;
using System.Linq;

namespace Wappa.Infrastructure.Service.Implementation
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleMapsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async System.Threading.Tasks.Task<(double, double)> GetLatitudeLongitude(string address)
        {
            var httpClient = _httpClientFactory.CreateClient("GoogleAPI");
            var response = await httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {

                var json = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                var lat = (double)json.SelectToken("results[0].geometry.location.lat");
                var lon = (double)json.SelectToken("results[0].geometry.location.lng");
                return (lat, lon);
            }

            return (0, 0);
        }
    }
}
