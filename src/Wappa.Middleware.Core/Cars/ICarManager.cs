using Wappa.Middleware.Domain.Cars;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Middleware.Core.Cars
{
    public interface ICarManager
    {
        IQueryable<Car> Cars { get; }

        Task<int?> CreateAsync(Car driver);

        Task UpdateAsync(Car driver);

        Task DeleteAsync(Car driver);

        Task<Car> GetByIdAsync(int id);

    }
}
