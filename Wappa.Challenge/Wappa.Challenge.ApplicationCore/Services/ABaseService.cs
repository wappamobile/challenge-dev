using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.ApplicationCore.Interfaces.Repositories;
using Wappa.Challenge.ApplicationCore.Interfaces.Services;

namespace Wappa.Challenge.ApplicationCore.Services
{
    public abstract class ABaseService<TEntity> : IBaseService<TEntity> where TEntity : ABaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;

        public ABaseService(IBaseRepository<TEntity> repository_)
        {
            _repository = repository_;
        }

        public virtual TEntity Obter(long id)
        {
            return _repository.Obter(id);
        }

        public virtual IEnumerable<TEntity> Listar()
        {
            return _repository.Listar();
        }

        public virtual IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Listar(predicate);
        }

        public virtual TEntity UmOuPadrao(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.UmOuPadrao(predicate);
        }

        public virtual TEntity Adicionar(TEntity entity)
        {
            return _repository.Adicionar(entity);
        }

        public virtual TEntity Alterar(TEntity entity)
        {
            return _repository.Alterar(entity);
        }

        public virtual IEnumerable<TEntity> AdicionarRange(IEnumerable<TEntity> entities)
        {
            return _repository.AdicionarRange(entities);
        }

        public virtual bool Apagar(TEntity entity)
        {
            return _repository.Apagar(entity);
        }

        public virtual bool ApagarRange(IEnumerable<TEntity> entities)
        {
            return _repository.ApagarRange(entities);
        }
    }
}
