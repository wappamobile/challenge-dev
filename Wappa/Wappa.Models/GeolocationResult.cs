using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.Models
{
    public class Root
    {
        [JsonProperty(PropertyName = "results")]
        public List<CoordenadasResultado> Resultados { get; set; }
    }

    public class CoordenadasResultado
    {
        [JsonProperty(PropertyName = "geometry")]
        public Coordenadas Coordenadas { get; set; }
    }

    public class Coordenadas
    {
        [JsonProperty(PropertyName = "location")]
        public Localizacao Localizacao { get; set; }
    }

    public class Localizacao
    {
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double Longitude { get; set; }
    }
}
