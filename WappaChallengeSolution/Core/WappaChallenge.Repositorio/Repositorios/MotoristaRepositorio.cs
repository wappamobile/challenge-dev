using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class MotoristaRepositorio : BaseRepositorio<Motorista, int>, IMotoristaRepositorio
    {
        public MotoristaRepositorio(IDatabase<Motorista, int> database) : base(database)
        {
        }
    }
}
