using ApplicationCore.Services.APIClient.Geocode.Model;
using Newtonsoft.Json;

namespace ApplicationCore.Services.APIClient.Geocode.Response
{
    public class GeocodeAddressResponse
    {
        [JsonProperty("results")]
        public GeocodeResult[] Results { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
