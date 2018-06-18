using ApplicationCore.Entity;
using PagedList.Core;
namespace ApplicationCore.Interfaces.Repository
{
    public interface IMotoristaRepository : IRepository<Motorista>
    {
        IPagedList<Motorista> Listar(int pageNumber, int pageSize, string sortBy);
    }
}
