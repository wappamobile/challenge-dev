using Newtonsoft.Json;

namespace WappaMobile.Driver.BackgroundTasks.DataTransferObject.Geocode
{
    public class Coordinate
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }
}
