namespace Wappa.Models.Geolocalizacao {

    public class Rootobject {
        public Result[] results { get; set; }
        public string status { get; set; }
    }

    public class Result {
        public Geometry geometry { get; set; }
    }

    public class Geometry {
        public Location location { get; set; }
    }

    public class Location {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}