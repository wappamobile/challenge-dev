
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Driver.Api.Dtos;

namespace Wappa.Driver.Api.Repositories.Interfaces
{
    /// <summary>
    /// Interface crud do motorista
    /// </summary>
    public interface IDriverRepository<T> : ICrudRepository<T>
    {
        /// <summary>
        /// Retorna a lista de motoristas
        /// </summary>
        /// <returns></returns>
        Task<List<DriverDto>> GetDrivers();
        /// <summary>
        /// Verifica se o motorista existe
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <returns></returns>
        Task<bool> Exists(int id);
    }
}
