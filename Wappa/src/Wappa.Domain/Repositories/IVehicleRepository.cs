using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Models;

namespace Wappa.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task<IVehicle> SaveAsync(IVehicle vehicle);

        Task<bool> DeleteAsync(IVehicle vehicle);

        Task<IEnumerable<IVehicle>> GetByDriverIdAsync(int driverId);
    }
}