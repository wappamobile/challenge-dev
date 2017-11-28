using System;
using System.Collections.Generic;
using WappaChallenge.Dominio.Entidades;

namespace WappaChallenge.Dominio.Interfaces.Repositorio
{
    public interface IBaseRepositorio<T> where T : BaseDominio
    {
        T Cadastrar(T entidade);
        T Atualizar(T entidade);
        void Excluir(int entidadeId);
        T BuscarPorId(int entidadeId);
        IEnumerable<T> Buscar(Func<T, bool> query);
        IEnumerable<T> ObterTodos();
    }
}
