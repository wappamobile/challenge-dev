using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class VeiculoRepositorio : BaseRepositorio<Veiculo>, IVeiculoRepositorio
    {
        public VeiculoRepositorio(IDatabase database) : base(database)
        {
        }
    }
}
