using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services.APIClient.Geocode.Model
{
    public class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}
