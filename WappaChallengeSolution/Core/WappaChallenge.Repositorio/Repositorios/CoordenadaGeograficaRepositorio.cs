using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class CoordenadaGeograficaRepositorio : BaseRepositorio<CoordenadaGeografica>, ICoordenadaGeograficaRepositorio
    {
        public CoordenadaGeograficaRepositorio(IDatabase database) : base(database)
        {
        }
    }
}
