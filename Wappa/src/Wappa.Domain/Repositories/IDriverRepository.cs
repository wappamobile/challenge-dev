using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Models;

namespace Wappa.Domain.Repositories
{
    public interface IDriverRepository
    {
        Task<IDriver> SaveAsync(IDriver driver);

        Task<bool> DeleteAsync(IDriver driver);

        Task<IDriver> GetByIdAsync(int? id);

        Task<IDriver> GetByDocumentAsync(string document);

        Task<IEnumerable<IDriver>> GetSearchAsync(IDriver driver);

        Task<bool> HasDriverAsync(int? id);

        Task<bool> HasDriverAsync(string document);
    }
}