using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wappa.Dominio.Repositorio;
using Wappa.Infraestrutura.Contexto;

namespace Wappa.Infraestrutura.Repositorio
{
    public class Repositorio<T> : IRepositorio<T>, IDisposable where T : class
    {
        private WappaContexto _contexto;

        public Repositorio(WappaContexto contexto)
        {
            this._contexto = contexto;
        }

        public bool Atualizar(T objeto, bool commit = true)
        {
            _contexto.Set<T>().Update(objeto);

            if (commit)
                return _contexto.SaveChanges() > 0;

            return false;
        }

        public bool Excluir(T objeto, bool commit = true)
        {
            _contexto.Set<T>().Remove(objeto);

            if (commit)
                return _contexto.SaveChanges() > 0;

            return false;
        }

        public bool Inserir(T objeto, bool commit = true)
        {
            _contexto.Set<T>().Add(objeto);

            if (commit)
                return _contexto.SaveChanges() > 0; ;

            return false;
        }

        public bool InserirLista(ICollection<T> objeto, bool commit = true)
        {
            _contexto.Set<T>().AddRange(objeto);

            if (commit)
                return _contexto.SaveChanges() > 0; ;

            return false;
        }

        public IQueryable<T> Query()
        {
            return _contexto.Set<T>();
        }

        public T Obter(Expression<Func<T, bool>> condicao)
        {
            return _contexto.Set<T>().AsNoTracking().FirstOrDefault(condicao);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_contexto != null)
                {
                    _contexto.Dispose();
                    _contexto = null;
                }
            }
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public bool ExcluirLista(ICollection<T> objeto, bool commit = true)
        {
            _contexto.Set<T>().RemoveRange(objeto);

            if (commit)
                return _contexto.SaveChanges() > 0;

            return false;
        }

        public bool AtualizarLista(ICollection<T> objeto, bool commit = true)
        {
            _contexto.Set<T>().UpdateRange(objeto);

            if (commit)
                return _contexto.SaveChanges() > 0;

            return false;
        }
    }
}