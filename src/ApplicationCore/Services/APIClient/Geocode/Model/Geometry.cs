using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services.APIClient.Geocode.Model
{
    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }
}
