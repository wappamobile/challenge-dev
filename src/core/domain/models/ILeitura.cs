using System;
using System.Collections.Generic;

namespace WappaMobile.ChallengeDev.Models
{
    public interface ILeitura<T> where T : Entidade
    {
        IEnumerable<T> Listar();
        T PegarPeloId(Guid id);
    }
}
