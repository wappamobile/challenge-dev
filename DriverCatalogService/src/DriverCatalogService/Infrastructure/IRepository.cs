using System.Threading.Tasks;
using DriverCatalogService.Models;

namespace DriverCatalogService.Infrastructure
{
    public interface IRepository
    {
        Task SetupTable();
        void DropTable();
        void Save(Driver driver);
        Driver Load(string driverId);
        bool Exists(string driverFirstName, string driverLastName);
        bool ContainsAnother(string driverId, string driverFirstName, string driverLastName);
        void Delete(string driverId);
        Driver[] List(string sortByField, string sortOrder);
    }
}