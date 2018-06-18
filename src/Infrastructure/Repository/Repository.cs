using ApplicationCore.Interfaces.Repository;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly Context _dbContext;

        public Repository(Context context)
        {
            _dbContext = context;
        }

        public void Add(TEntity obj)
        {
            _dbContext.Set<TEntity>().Add(obj);
        }

        public virtual TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual void Update(TEntity obj)
        {   
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Remove(int id)
        {
            _dbContext.Set<TEntity>().Remove(_dbContext.Set<TEntity>().Find(id));
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
