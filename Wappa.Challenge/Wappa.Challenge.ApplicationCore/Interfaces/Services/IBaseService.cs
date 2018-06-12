using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Wappa.Challenge.ApplicationCore.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        TEntity Obter(long id);
        IEnumerable<TEntity> Listar();
        IEnumerable<TEntity> Listar(Expression<Func<TEntity, bool>> predicate);
        TEntity UmOuPadrao(Expression<Func<TEntity, bool>> predicate);
        TEntity Adicionar(TEntity entity);
        TEntity Alterar(TEntity entity);
        IEnumerable<TEntity> AdicionarRange(IEnumerable<TEntity> entities);
        bool Apagar(TEntity entity);
        bool ApagarRange(IEnumerable<TEntity> entities);
    }
}
