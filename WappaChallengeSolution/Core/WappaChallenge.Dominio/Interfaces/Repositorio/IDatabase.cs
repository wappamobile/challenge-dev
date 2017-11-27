using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WappaChallenge.Dominio.Entidades;

namespace WappaChallenge.Dominio.Interfaces.Repositorio
{
    public interface IDatabase<T, TId> : IDisposable where T : BaseDominio<TId>
    {
        T Cadastrar(T entidade);
        T Atualizar(T entidade);
        void Excluir(TId entidadeId);
        T BuscarPorId(TId entidadeId);
        ICollection<T> Buscar(Expression<Func<T, bool>> query);
    }
}
