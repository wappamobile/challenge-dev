using System.Collections.Generic;

namespace Domain.Model
{
    public class GoogleGeocodingApiResponse
    {
        public List<GoogleGeocodingApiItem> results { get; set; }
        public string status { get; set; }
    }

    public class GoogleGeocodingApiItem
    {
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
