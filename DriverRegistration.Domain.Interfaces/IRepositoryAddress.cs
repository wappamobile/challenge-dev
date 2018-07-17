
namespace DriverRegistration.Domain.Interfaces
{
    public interface IRepositoryAddress
    {
        IAddress Add(IAddress address, int DriverId);

        bool Update(IAddress address);

        bool Delete(int id);

        IAddress Load(int DriverId);
    }
}
