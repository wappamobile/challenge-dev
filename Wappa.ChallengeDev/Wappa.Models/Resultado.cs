using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Models
{
    public class Resultado
    {
        [JsonProperty("geometry")]
        public Geometry Geolocalizacao
        {
            get;
            set;
        }
    }
}
