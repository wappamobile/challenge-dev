using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Contracts.Models;

namespace Wappa.Contracts
{
    public interface IDriverDB
    {
        Task DeleteDriver(string id);
        List<Driver> GetDriversOrderByFirstName();
        List<Driver> GetDriversOrderByLastName();
        Task SaveDriver(Driver driver);
        Task UpdateDriver(Driver driver);
    }
}
