using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Repositorios
{
    public class EnderecoRepositorio : BaseRepositorio<Endereco, int>, IEnderecoRepositorio
    {
        public EnderecoRepositorio(IDatabase<Endereco, int> database) : base(database)
        {
        }
    }
}
