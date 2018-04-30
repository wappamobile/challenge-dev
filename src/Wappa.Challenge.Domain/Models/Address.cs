
namespace Wappa.Challenge.Domain.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public void SetCoordinates((double? latitude, double? longitude) coordinates)
        {
            Latitude = coordinates.latitude.Value;
            Longitude = coordinates.longitude.Value;
        }
    }
}