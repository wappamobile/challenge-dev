using Wappa.Domain.Entities;

namespace Wappa.Domain.Repositories
{
    public interface ICarroRepository
    {
        Carro Obter(int id);
        Carro[] ListarTodos();
    }
}
