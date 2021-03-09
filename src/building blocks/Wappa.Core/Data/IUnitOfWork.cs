using System.Threading.Tasks;

namespace Wappa.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}