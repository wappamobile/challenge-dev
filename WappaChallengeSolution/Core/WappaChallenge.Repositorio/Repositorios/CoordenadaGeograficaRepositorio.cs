using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class CoordenadaGeograficaRepositorio : BaseRepositorio<CoordenadaGeografica, int>, ICoordenadaGeograficaRepositorio
    {
        public CoordenadaGeograficaRepositorio(IDatabase<CoordenadaGeografica, int> database) : base(database)
        {
        }
    }
}
