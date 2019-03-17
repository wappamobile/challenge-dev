namespace WappaMobile.ChallengeDev.GoogleMaps
{
    class Result
    {
        public Geometry geometry { get; set; }
        public string formatted_address { get; set; }
        public string place_id { get; set; }

        public override string ToString()
        {
            return geometry?.ToString();
        }
    }
}
