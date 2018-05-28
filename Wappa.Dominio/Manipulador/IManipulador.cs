using Wappa.Dominio.Comando;
using Wappa.Dominio.Resultado;

namespace Wappa.Dominio.Manipulador
{
    public interface IManipulador<T> where T : IComando
    {
        IResultadoComando Manipular(T comando);
    }
}