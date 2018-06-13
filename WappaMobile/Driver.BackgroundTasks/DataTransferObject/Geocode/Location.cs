using System;
using Newtonsoft.Json;

namespace WappaMobile.Driver.BackgroundTasks.DataTransferObject.Geocode
{
    public class Location
    {
        [JsonProperty("lat")]
        public Double Latitude { get; set; }

        [JsonProperty("lng")]
        public Double Longitude { get; set; }
    }
}
