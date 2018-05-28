using Wappa.Dominio.Entidade;
using Wappa.Dominio.Repositorio;
using Wappa.Infraestrutura.Contexto;

namespace Wappa.Infraestrutura.Repositorio
{
    public class CarroRepositorio : Repositorio<Carro>, ICarroRepositorio
    {
        private WappaContexto _contexto;

        public CarroRepositorio(WappaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}