namespace DriverRegistration.Domain.Interfaces
{
    public interface ICar
    {
        int Id { get; set; }

        string Brand { get; set; }

        string Model { get; set; }

        string Plate { get; set; }
    }
}
