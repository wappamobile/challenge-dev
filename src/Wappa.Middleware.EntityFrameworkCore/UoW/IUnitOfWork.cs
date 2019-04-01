using System;
using System.Threading.Tasks;

namespace Wappa.Middleware.EntityFrameworkCore.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
