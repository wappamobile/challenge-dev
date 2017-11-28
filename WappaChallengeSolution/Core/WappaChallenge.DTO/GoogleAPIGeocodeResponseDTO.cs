namespace WappaChallenge.DTO
{
    public class GoogleAPIGeocodeResponseDTO
    {
        public results[] results { get; set; }
    }

    public class results
    {
        public geometry geometry { get; set; }
    }

    public class geometry
    {
        public location location { get; set; }
    }

    public class location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}
