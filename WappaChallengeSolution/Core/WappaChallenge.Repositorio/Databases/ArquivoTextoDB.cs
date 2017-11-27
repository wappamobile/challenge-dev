using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Databases
{
    public class ArquivoTextoDB<T, TId> : IDatabase<T, TId> where T : BaseDominio<TId>
    {
        public T Atualizar(T entidade)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Buscar(Expression<Func<T, bool>> query)
        {
            throw new NotImplementedException();
        }

        public T BuscarPorId(TId entidadeId)
        {
            throw new NotImplementedException();
        }

        public T Cadastrar(T entidade)
        {
            throw new NotImplementedException();
        }

        public void Excluir(TId entidadeId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
