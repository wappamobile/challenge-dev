using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WappaMobile.Driver.API.Services.DataTransferObject.Geocode
{
    public class Location
    {
        [JsonProperty("lat")]
        public Double Latitude { get; set; }

        [JsonProperty("lng")]
        public Double Longitude { get; set; }
    }
}
