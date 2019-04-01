using Wappa.Middleware.Domain.Drivers;
using Wappa.Middleware.EntityFrameworkCore.Contexts;
using DotNetCore.EntityFrameworkCore;

namespace Wappa.Middleware.EntityFrameworkCore.Repositories.Drivers
{
    public sealed class DriverRepository : EntityFrameworkCoreRepository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext appContext)
            : base(appContext)
        {

        }
    }
}
