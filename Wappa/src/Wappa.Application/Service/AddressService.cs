using System.Threading.Tasks;
using Wappa.Domain.Interfaces.Connectors;
using Wappa.Domain.Models;

namespace Wappa.Application.Service
{
    public static class AddressService
    {
        public static async Task ApplyLocationAsync(
            IGoogleMapsConnector googleMaps, Address address)
        {
            var location = await googleMaps.GetLocationAsync(address);

            address.SetLocation(location);
        }
    }
}