using Newtonsoft.Json;

namespace WappaMobile.Driver.BackgroundTasks.DataTransferObject.Geocode
{
    public class Geometry
    {
        [JsonProperty("bounds")]
        public Coordinate Bounds { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("viewport")]
        public Coordinate Viewport { get; set; }
    }
}
