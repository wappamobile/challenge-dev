using Wappa.Middleware.Core.GoogleMaps.VO;
using Wappa.Middleware.Domain.Drivers;
using System.Threading.Tasks;

namespace Wappa.Middleware.Core.GoogleMaps
{
    public interface IGoogleMapManager
    {
        Task<GoogleGeocodeOutputVO> GeocodeAdress(Driver driver);
    }
}
