namespace DriverRegistration.Domain.Interfaces
{
    public interface IAddress
    {
        int Id { get; set; }

        string AddressName { get; set; }

        int Number { get; set; }

        string Neighborhood { get; set; }

        string PostalCode { get; set; }

        string State { get; set; }

        decimal Longitude { get; set; }

        decimal Latitude { get; set; }

        string City { get; set; }
    }
}
