using Wappa.Domain.Entities;

namespace Wappa.Domain.Repositories
{
    public interface ICidadeRepository
    {
        Cidade Obter(int id);
        Cidade[] ListarTodos();
    }
}
