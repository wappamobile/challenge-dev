using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class MotoristaRepositorio : BaseRepositorio<Motorista>, IMotoristaRepositorio
    {
        public MotoristaRepositorio(IDatabase database) : base(database)
        {
        }
    }
}
