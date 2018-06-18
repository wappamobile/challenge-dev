using ApplicationCore.Entity;
using PagedList.Core;

namespace ApplicationCore.Interfaces.Services
{
    public interface IMotoristaService
    {
        void Add(Motorista motorista);
        void Update(Motorista motorista);
        Motorista Obter(int motoristaId);
        void Delete(int motoristaId);
        IPagedList<Motorista> Listar(int pageNumber, int pageSize, string sortBy);
    }
}
