using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class VeiculoRepositorio : BaseRepositorio<Veiculo, int>, IVeiculoRepositorio
    {
        public VeiculoRepositorio(IDatabase<Veiculo, int> database) : base(database)
        {
        }
    }
}
