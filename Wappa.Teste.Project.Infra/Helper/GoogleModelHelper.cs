using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wappa.Teste.Project.Infra.Helper
{
    public class GoogleModelHelper
    {
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public float Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public float Longitude { get; set; }
    }
}
