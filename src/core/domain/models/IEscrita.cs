using System;

namespace WappaMobile.ChallengeDev.Models
{
    public interface IEscrita<T> where T : Entidade
    {
        void Incluir(T entidade);
        void Atualizar(T entidade);
        void Excluir(Guid id);
    }
}
