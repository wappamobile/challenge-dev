using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Wappa.Core.Repositories
{
    public class RepositoryBase<T, DB> : IDisposable, IRepositoryBase<T> where T : class
                                                                         where DB : DbContext
    {
        protected DB _context;
        private bool _disposed = false;

        public RepositoryBase(DB context)
        {
            _context = context;
        }

        public async Task<T> Add(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return obj;
        }

        public void Remove(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Exists(string id)
        {
            var obj = await _context.Set<T>().FindAsync(id);
            if (obj == null)
                return false;
            else
                return true;
        }

        public T Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            return obj;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _context != null)
            {
                _context.Dispose();
                _context = null;
            }

            _disposed = true;
        }

    }
}
