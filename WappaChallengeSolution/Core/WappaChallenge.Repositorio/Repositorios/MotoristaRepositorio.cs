using System.Collections.Generic;
using System.Linq;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class MotoristaRepositorio : BaseRepositorio<Motorista>, IMotoristaRepositorio
    {
        public MotoristaRepositorio(IDatabase database) : base(database)
        {
        }

        public ICollection<Motorista> ObterTodosOrdenadoPorPrimeiroNome()
        {
            return this.ObterTodos().OrderBy(c => c.PrimeiroNome).ToList();
        }

        public ICollection<Motorista> ObterTodosOrdenadoPorUltimoNome()
        {
            return this.ObterTodos().OrderBy(c => c.UltimoNome).ToList();
        }
    }
}
