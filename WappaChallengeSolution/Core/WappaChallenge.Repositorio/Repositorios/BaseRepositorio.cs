using System;
using System.Collections.Generic;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public abstract class BaseRepositorio<T> : IBaseRepositorio<T> where T : BaseDominio
    {
        private readonly IDatabase _database;

        public BaseRepositorio(IDatabase database)
        {
            this._database = database;
        }

        public T Atualizar(T entidade)
        {
            return this._database.Atualizar(entidade);
        }

        public IEnumerable<T> Buscar(Func<T, bool> query)
        {
            return this._database.Buscar(query);
        }

        public T BuscarPorId(int entidadeId)
        {
            return this._database.BuscarPorId<T>(entidadeId);
        }

        public T Cadastrar(T entidade)
        {
            return this._database.Cadastrar(entidade);
        }

        public void Excluir(int entidadeId)
        {
            this._database.Excluir<T>(entidadeId);
        }

        public IEnumerable<T> ObterTodos()
        {
            return this._database.ObterTodos<T>();
        }
    }
}
