using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Challenge.Dev.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        T Find(long id);
        T First(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllOrderBy(Func<T, object> keySelector);
        IEnumerable<T> GetAllOrderByDescending(Func<T, object> keySelector);
        bool Update(long id, T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
