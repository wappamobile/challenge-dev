using Wappa.Domain.Interfaces.Models;

namespace Wappa.Domain.Models
{
    public class Vehicle : IVehicle
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public string Fabricator { get; set; }
    }
}