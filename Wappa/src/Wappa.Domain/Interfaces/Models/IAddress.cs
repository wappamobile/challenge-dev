namespace Wappa.Domain.Interfaces.Models
{
    public interface IAddress
    {
        int Id { get; set; }
        int DriverId { get; set; }
        string PostalCode { get; set; }
        string StreetName { get; set; }
        string Number { get; set; }
        string City { get; set; }
        string Neighborhood { get; set; }
        string StateCode { get; set; }
        string Country { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
    }
}