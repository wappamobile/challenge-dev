namespace WappaMobile.ChallengeDev.Models
{
    public struct Coordenadas
    {
        public double Latitude, Longitude;

        public Coordenadas(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }

        public override string ToString()
        {
            return $"{Latitude}x{Longitude}";
        }
    }
}
