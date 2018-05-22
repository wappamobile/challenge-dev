using System.Threading.Tasks;
using Wappa.Core.Models;

namespace Wappa.Core.Interfaces
{
    public interface ICoordenadasRepository
    {    
        Task<CoordenadasData> Get(Endereco endereco); 
    }
}