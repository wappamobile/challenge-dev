using Wappa.Domain.Interfaces.Connectors;

namespace Wappa.Domain.Models
{
    public class Location : ILocation
    {
        public Location(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}