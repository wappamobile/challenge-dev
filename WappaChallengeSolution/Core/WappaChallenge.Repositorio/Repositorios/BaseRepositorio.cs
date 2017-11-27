using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public abstract class BaseRepositorio<T, TId> : IBaseRepositorio<T, TId> where T : BaseDominio<TId>
    {
        private readonly IDatabase<T, TId> _database;

        public BaseRepositorio(IDatabase<T, TId> database)
        {
            this._database = database;
        }

        public T Atualizar(T entidade)
        {
            return this._database.Atualizar(entidade);
        }

        public ICollection<T> Buscar(Expression<Func<T, bool>> query)
        {
            return this._database.Buscar(query);
        }

        public T BuscarPorId(TId entidadeId)
        {
            return this._database.BuscarPorId(entidadeId);
        }

        public T Cadastrar(T entidade)
        {
            return this._database.Cadastrar(entidade);
        }

        public void Excluir(TId entidadeId)
        {
            this._database.Excluir(entidadeId);
        }
    }
}
