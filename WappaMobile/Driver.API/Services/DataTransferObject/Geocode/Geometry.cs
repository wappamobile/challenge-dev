using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WappaMobile.Driver.API.Services.DataTransferObject.Geocode
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
