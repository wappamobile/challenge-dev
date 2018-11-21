using Newtonsoft.Json;

namespace Wappa.Domain.Interfaces.Models
{
    public class GoogleMapsRoot
    {
        [JsonProperty(PropertyName = "results")]
        public GoogleMapsResult[] Results { get; set; }
    }

    public class GoogleMapsResult
    {
        [JsonProperty(PropertyName = "geometry")]
        public GoogleMapsGeometry Geometry { get; set; }
    }

    public class GoogleMapsGeometry
    {
        [JsonProperty(PropertyName = "location")]
        public GoogleMapsLocation Location { get; set; }
    }

    public class GoogleMapsLocation
    {
        [JsonProperty(PropertyName = "lat")]
        public float Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public float Longtude { get; set; }
    }
}