using Newtonsoft.Json;

namespace ApplicationCore.Services.APIClient.Geocode.Model
{
    public class GeocodeResult
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }
}
