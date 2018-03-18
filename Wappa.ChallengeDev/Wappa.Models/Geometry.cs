using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Models
{
    public class Geometry
    {
        [JsonProperty("location")]
        public Localizacao Localizacao
        {
            get;
            set;
        }
    }
}
