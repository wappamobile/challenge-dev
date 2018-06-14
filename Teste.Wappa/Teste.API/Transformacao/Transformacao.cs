using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Implementacao.Inteface;

namespace Teste.API.Transformacao
{
    public abstract class Transformacao<T> : Interface.ITransformacao<T>
    {
        public virtual IEnumerable<T> Transformar(IEnumerable<IDTO> implementacao)
        {
            var listaRetorno = new List<T>();

            implementacao.ToList().ForEach(item => listaRetorno.Add(Transformar(item)));

            return listaRetorno;
        }

        public abstract IDTO Transformar(T implementacao);

        public abstract T Transformar(IDTO entidade);
    }
}
