using System.Threading.Tasks;

namespace Wappa.Driver.Api.Repositories.Interfaces
{
    /// <summary>
    /// Interface base para CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudRepository<T>
    {
        /// <summary>
        /// Insere um objeto
        /// </summary>
        /// <param name="obj">Objeto a se incluído</param>
        /// <returns></returns>
        Task<int> Insert(T obj);
        /// <summary>
        /// Atualiza um objeto
        /// </summary>
        /// <param name="obj">Objeto a ser atualiizdo</param>
        /// <returns></returns>
        Task<T> Update(T obj);
        /// <summary>
        /// Exclui um objeto
        /// </summary>
        /// <param name="id">Id do objeto a ser excluído</param>
        /// <returns></returns>
        Task Delete(int id);
    }
}
