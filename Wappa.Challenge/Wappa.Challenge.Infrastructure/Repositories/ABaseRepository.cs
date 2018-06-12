using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.Infrastructure.Context;

namespace Wappa.Challenge.Infrastructure.Repositories
{
    public abstract class ABaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : ABaseEntity
    {
        public ABaseRepository() : base() { }

        public virtual TEntity Obter(long id)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    return Context.Set<TEntity>().Find(id);
                }
            }
            catch
            {
                return null;
            }
        }

        public virtual IEnumerable<TEntity> Listar()
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    var model = Context.Set<TEntity>().ToList();

                    return model;
                }
            }
            catch (Exception ex)
            {
                return new List<TEntity>();
            }

        }

        public virtual IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    return Context.Set<TEntity>().Where(predicate);
                }
            }
            catch
            {
                return new List<TEntity>();
            }
        }

        public virtual TEntity UmOuPadrao(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    return Context.Set<TEntity>().SingleOrDefault(predicate);
                }
            }
            catch
            {
                return null;
            }
        }

        public virtual TEntity Adicionar(TEntity entity)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    Context.Set<TEntity>().Add(entity);
                    Context.SaveChanges();
                }

                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual TEntity Alterar(TEntity entity)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    Context.Set<TEntity>().Attach(entity);
                    Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    Context.SaveChanges();
                }

                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual IEnumerable<TEntity> AdicionarRange(IEnumerable<TEntity> entities)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    Context.Set<TEntity>().AddRange(entities);
                    Context.SaveChanges();
                }

                return entities;
            }
            catch
            {
                return null;
            }
        }

        public virtual bool Apagar(TEntity entity)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    //Context.Set<TEntity>().Remove(entity);

                    Context.Set<TEntity>().Attach(entity);
                    Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    Context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool ApagarRange(IEnumerable<TEntity> entities)
        {
            try
            {
                using (var Context = new DatabaseContext())
                {
                    Context.Set<TEntity>().RemoveRange(entities);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
