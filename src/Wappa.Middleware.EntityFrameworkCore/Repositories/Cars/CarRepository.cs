using Wappa.Middleware.Domain.Cars;
using Wappa.Middleware.EntityFrameworkCore.Contexts;
using DotNetCore.EntityFrameworkCore;

namespace Wappa.Middleware.EntityFrameworkCore.Repositories.Cars
{
    public class CarRepository : EntityFrameworkCoreRelationalRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext appContext)
            : base(appContext)
        {

        }
    }
}
