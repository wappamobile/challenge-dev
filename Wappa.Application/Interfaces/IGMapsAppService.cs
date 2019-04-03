using System.Threading.Tasks;
using Wappa.Domain.Models;

namespace Wappa.Application.Interfaces
{
    public interface IGMapsAppService
    {
        Task<ValueObjectsGMaps> GetCoordinates(string address);
    }
}
