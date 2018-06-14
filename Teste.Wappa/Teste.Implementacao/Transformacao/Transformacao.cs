using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.Implementacao.Inteface;
using Teste.Repositorio.Interface;

namespace Teste.Implementacao.Transformacao
{
    public abstract class Transformacao<T> : ITransformacao<T>
    {
        public virtual IEnumerable<T> Transformar(IEnumerable<IEntidade> implementacao)
        {
            var listaRetorno = new List<T>();
            
            implementacao.ToList().ForEach(item => listaRetorno.Add(Transformar(item)));

            return listaRetorno;
        }

        public abstract IEntidade Transformar(T implementacao);

        public abstract T Transformar(IEntidade entidade);
    }
}
