using Wappa.Middleware.Domain.Drivers;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Middleware.Core.Drivers
{
    public interface IDriverManager
    {
        IQueryable<Driver> Drivers { get; }

        Task<int?> CreateAsync(Driver driver);

        Task UpdateAsync(Driver driver);

        Task DeleteAsync(Driver driver);

        Task<Driver> GetByIdAsync(int id);

    }
}
