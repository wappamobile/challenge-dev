using Wappa.Middleware.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Wappa.Middleware.EntityFrameworkCore.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw new System.Exception(ex.Message);
            }
            
        }

        public Task<int> SaveChangesAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                throw new System.Exception(ex.Message);
            }
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
