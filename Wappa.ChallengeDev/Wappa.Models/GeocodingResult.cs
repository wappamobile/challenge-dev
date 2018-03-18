using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Wappa.Models
{
    public class GeocodingResult
    {
        [JsonProperty("Results")]
        public List<Resultado> Resultados
        {
            get;
            set;
        }

        [JsonProperty("status")]
        public string Status
        {
            get;
            set;
        }
    }
}
