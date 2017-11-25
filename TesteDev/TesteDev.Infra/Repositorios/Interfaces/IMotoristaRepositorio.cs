using System.Collections.Generic;
using TesteDev.Infra.Entidades;

namespace TesteDev.Infra.Repositorios.Interfaces
{
    public interface IMotoristaRepositorio :  IRepositorioBase<Motorista>
    {
        IList<Motorista> Listar(string nome, string ultimoNome);
    }
}
