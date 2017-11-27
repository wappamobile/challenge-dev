using System;
using System.Collections.Generic;
using WappaChallenge.Dominio.Entidades;

namespace WappaChallenge.Dominio.Interfaces.Repositorio
{
    public interface IDatabase : IDisposable
    {
        T Cadastrar<T>(T entidade) where T : BaseDominio;
        T Atualizar<T>(T entidade) where T : BaseDominio;
        void Excluir<T>(int entidadeId) where T : BaseDominio; 
        T BuscarPorId<T>(int entidadeId) where T : BaseDominio;
        IEnumerable<T> Buscar<T>(Func<T, bool> query) where T : BaseDominio;
        IEnumerable<T> ObterTodos<T>() where T : BaseDominio;
    }
}
