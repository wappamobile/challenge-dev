using System.Threading.Tasks;
using WappaMobile.Application.Services.Geocoding;
using WappaMobile.Domain;

namespace WappaMobile.Application.Tests
{
    public class DummyGeocoder : IGeocoder
    {
        private readonly Coordinates coordinates;

        public DummyGeocoder(Coordinates coordinates)
        {
            this.coordinates = coordinates;
        }

        public Task<Coordinates> GetCoordinatesForAddress(Address address)
        {
            return Task.FromResult(coordinates);
        }
    }
}
