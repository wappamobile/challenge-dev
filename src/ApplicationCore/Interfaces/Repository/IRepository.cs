using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetQuery();
        void Update(TEntity obj);
        void Remove(int id);
        int SaveChanges();
    }
}
