using System.Collections.Generic;

namespace Wappa.Challenge.Domain.Commands.Outputs
{
    public class CoordinatesResult
    {
        public List<GoogleResult> Results { get; set; }
    }

    public class GoogleResult
    {
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }

        public double Lng { get; set; }
    }

}