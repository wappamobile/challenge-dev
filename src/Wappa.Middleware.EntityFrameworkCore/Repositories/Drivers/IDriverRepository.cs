using Wappa.Middleware.Domain.Drivers;
using DotNetCore.Repositories;

namespace Wappa.Middleware.EntityFrameworkCore.Repositories.Drivers
{
    public interface IDriverRepository : IRepository<Driver>
    {
    }
}
