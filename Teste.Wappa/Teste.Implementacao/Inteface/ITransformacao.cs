using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Interface;

namespace Teste.Implementacao.Inteface
{
    public interface ITransformacao<T>
    {
        IEnumerable<T> Transformar(IEnumerable<IEntidade> entidade);
        IEntidade Transformar(T implementacao);
        T Transformar(IEntidade entidade);
    }
}
