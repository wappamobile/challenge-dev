using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Implementacao.Inteface;

namespace Teste.API.Interface
{
    public interface ITransformacao<T>
    {
        IEnumerable<T> Transformar(IEnumerable<IDTO> entidade);
        IDTO Transformar(T implementacao);
        T Transformar(IDTO entidade);
    }
}
