namespace Wappa.Domain.Interfaces.Models
{
    public interface IVehicle
    {
        int Id { get; set; }
        int DriverId { get; set; }
        string Plate { get; set; }
        string Model { get; set; }
        string Fabricator { get; set; }
    }
}