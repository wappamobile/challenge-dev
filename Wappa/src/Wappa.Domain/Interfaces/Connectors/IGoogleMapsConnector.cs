using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Models;

namespace Wappa.Domain.Interfaces.Connectors
{
    public interface IGoogleMapsConnector
    {
        Task<ILocation> GetLocationAsync(IAddress address);
    }
}