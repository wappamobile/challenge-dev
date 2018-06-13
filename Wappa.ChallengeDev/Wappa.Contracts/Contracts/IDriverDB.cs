using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Contracts.Models;

namespace Wappa.Contracts
{
    public interface IDriverDB
    {
        Task SaveDriver(Driver driver);
        Task UpdateDriver(Driver driver);
        Task DeleteDriver(string id);
        List<Driver> GetDriversOrderByFirstName();
        List<Driver> GetDriversOrderByLastName();
    }
}
