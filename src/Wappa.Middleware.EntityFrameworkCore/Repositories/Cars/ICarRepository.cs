using Wappa.Middleware.Domain.Cars;
using DotNetCore.Repositories;

namespace Wappa.Middleware.EntityFrameworkCore.Repositories.Cars
{
    public interface ICarRepository : IRelationalRepository<Car>
    {
    }
}
