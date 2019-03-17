using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Infrastructure.Data.Models;

namespace Wappa.Infrastructure.Data.Interface
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAllDrivers();

        // api/1/[GET]
        Task<Driver> GetDriver(string id);

        // api/[POST]
        Task Create(Driver driver);

        // api/[PUT]
        Task<bool> Update(Driver driver);

        // api/1/[DELETE]
        Task<bool> Delete(string id);

        Task<long> GetNextId();

    }
}
