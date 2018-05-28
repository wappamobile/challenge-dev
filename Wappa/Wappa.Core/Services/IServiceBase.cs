using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Wappa.Core.Services
{
    public interface IServiceBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> GetById(int id);
        Task<bool> Exists(string id);
        Task<T> Add(T obj);
        T Update(T obj);
        void Remove(T obj);
        void Dispose();
    }
}
