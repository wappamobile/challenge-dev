using Wappa.Domain.Interfaces;
using Wappa.Infra.Data.Context;

namespace Wappa.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WappaContext _context;

        public UnitOfWork(WappaContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
