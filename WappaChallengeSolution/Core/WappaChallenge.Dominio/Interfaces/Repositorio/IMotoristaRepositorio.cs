using System.Collections;
using System.Collections.Generic;
using WappaChallenge.Dominio.Entidades;

namespace WappaChallenge.Dominio.Interfaces.Repositorio
{
    public interface IMotoristaRepositorio : IBaseRepositorio<Motorista>
    {
        ICollection<Motorista> ObterTodosOrdenadoPorPrimeiroNome();

        ICollection<Motorista> ObterTodosOrdenadoPorUltimoNome();
    }
}
