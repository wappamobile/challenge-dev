using Wappa.Dominio.Entidade;
using Wappa.Dominio.Repositorio;
using Wappa.Infraestrutura.Contexto;

namespace Wappa.Infraestrutura.Repositorio
{
    public class EnderecoRepositorio : Repositorio<Endereco>, IEnderecoRepositorio
    {
        private WappaContexto _contexto;

        public EnderecoRepositorio(WappaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}