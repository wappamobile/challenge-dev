using System.Collections.Generic;

namespace Wappa.Challenge.Dev.GoogleMaps.Services.Impl.Model
{
    public class Result
    {
        public List<AddressComponent> AddressComponents { get; set; }
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        public string PlaceId { get; set; }
        public List<string> Types { get; set; }
    }
}