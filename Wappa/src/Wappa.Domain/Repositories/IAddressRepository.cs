using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Models;

namespace Wappa.Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<IAddress> SaveAsync(IAddress address);

        Task<bool> DeleteAsync(IAddress address);

        Task<IEnumerable<IAddress>> GetByDriverIdAsync(int driverId);
    }
}