using System.Collections.Generic;
using System.Threading.Tasks;

namespace Driver.Application.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface base dos repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        Task<bool> DeleteAsync(int id);

        Task<List<T>> GetAsync();

        Task<T> GetBydIdAsync(int id);

        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(T entity);
    }
}