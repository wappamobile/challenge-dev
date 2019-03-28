using System;
using System.Collections.Generic;
using System.Text;

namespace DriverRegistration.Domain.Services.ValueObjects
{
    public class GeocodeResponseVO
    {
        public Result[] Results { get; set; }
        public string Status { get; set; }
    }

    public class Result
    {
        public AddressComponents[] AddressComponents { get; set; }
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        public string PlaceId { get; set; }
        public string[] Types { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
        public string LocationType { get; set; }
        public Viewport Viewport { get; set; }
    }

    public class Location
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
    }

    public class Viewport
    {
        public Northeast Northeast { get; set; }
        public Southwest Southwest { get; set; }
    }

    public class Northeast
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
    }

    public class Southwest
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
    }

    public class AddressComponents
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string[] Types { get; set; }
    }
}
