using System.Collections.Generic;

namespace Wappa.Challenge.Dev.GoogleMaps.Services.Impl.Model
{
    public class GeoCodeResult
    {
        public List<Result> Results { get; set; }
        public string Status { get; set; }
    }
}
