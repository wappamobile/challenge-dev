using System.Collections.Generic;

namespace Wappa.Challenge.Dev.GoogleMaps.Services.Impl.Model
{
    public class AddressComponent
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public List<string> Types { get; set; }
    }
}