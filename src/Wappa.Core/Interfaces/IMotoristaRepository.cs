using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Core.Models;

namespace Wappa.Core.Interfaces
{
    public interface IMotoristaRepository
    {    
        Task<Motorista> Get(int id);

        Task<IEnumerable<Motorista>> GetAll(string ordem);

        Task<Motorista> Save(Motorista motorista);

        Task<Motorista> Update(Motorista motorista);

        Task<int> Delete(int id);        
    }
}