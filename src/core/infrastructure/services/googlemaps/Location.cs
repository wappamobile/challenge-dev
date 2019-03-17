namespace WappaMobile.ChallengeDev.GoogleMaps
{
    class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }

        public override string ToString()
        {
            return $"{lat}x{lng}";
        }
    }
}
