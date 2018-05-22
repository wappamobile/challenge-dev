using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wappa.Core.Models
{
    public class CoordenadasData
    {
        [JsonProperty("results")]
        public IEnumerable<Coordenadas> Resultados { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Geometria
    {
        [JsonProperty("location")]
        public Localizacao Localizacao{ get; set; }
    }

    public class Localizacao
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }

    public class Coordenadas
    {
        [JsonProperty("geometry")]        
        public Geometria Geometria { get; set; }        
    }
}