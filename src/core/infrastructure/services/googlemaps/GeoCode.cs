namespace WappaMobile.ChallengeDev.GoogleMaps
{
    class GeoCode
    {
        public Result[] results { get; set; }
        public string status { get; set; }

        public double lat
        {
            get
            {
                return results.Length == 0 ? 0 : results[0].geometry.location.lat;
            }
        }
        public double lng
        {
            get
            {
                return results.Length == 0 ? 0 : results[0].geometry.location.lng;
            }
        }
    }
}