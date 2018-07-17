
namespace DriverRegistration.Domain.Interfaces
{
    public interface IDriver
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        ICar DriverCar { get; set; }

        IAddress Address { get; set; }
    }
}
