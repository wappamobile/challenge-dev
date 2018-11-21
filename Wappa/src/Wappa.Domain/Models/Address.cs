using Wappa.Domain.Interfaces.Connectors;
using Wappa.Domain.Interfaces.Models;

namespace Wappa.Domain.Models
{
    public class Address : IAddress
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public void SetLocation(ILocation location)
        {
            Longitude = location.Longitude;
            Latitude = location.Latitude;
        }
    }
}