using System;
using System.Collections.Generic;

namespace Wappa.Challenge.Dev.GoogleMaps.Services.Impl.Configuration
{
    public class GoogleMapsConfiguration
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public Dictionary<string, string> Resources { get; set; }
    }
}
