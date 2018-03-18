using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Models
{
    public class Localizacao
    {
        [JsonProperty("lat")]
        public string Latitude
        {
            get;
            set;
        }

        [JsonProperty("lng")]
        public string Longitude
        {
            get;
            set;
        }
    }
}
