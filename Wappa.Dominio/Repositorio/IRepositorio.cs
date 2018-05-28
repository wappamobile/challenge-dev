using System;
using System.Collections.Generic;
using System.Linq;

namespace Wappa.Dominio.Repositorio
{
    public interface IRepositorio<T> : IDisposable where T : class
    {
        IQueryable<T> Query();

        bool Inserir(T objeto, bool commit = true);

        bool InserirLista(ICollection<T> objeto, bool commit = true);

        bool Atualizar(T objeto, bool commit = true);

        bool Excluir(T objeto, bool commit = true);

        bool ExcluirLista(ICollection<T> objeto, bool commit = true);

        bool AtualizarLista(ICollection<T> objeto, bool commit = true);

        void Dispose();

        void Commit();
    }
}