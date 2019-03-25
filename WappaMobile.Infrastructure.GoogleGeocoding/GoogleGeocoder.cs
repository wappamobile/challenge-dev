using System.Linq;
using System.Threading.Tasks;
using WappaMobile.Application.Services.Geocoding;
using WappaMobile.Domain;

namespace WappaMobile.Infrastructure.GoogleGeocoding
{
    /// <summary>
    /// Google Maps-based geocoding service.
    /// </summary>
    public class GoogleGeocoder : IGeocoder
    {
        private readonly Geocoding.Google.GoogleGeocoder _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WappaMobile.Infrastructure.GoogleGeocoding.GoogleGeocoder"/> class.
        /// </summary>
        /// <param name="apiKey">API key.</param>
        public GoogleGeocoder(string apiKey)
        {
            _client = new Geocoding.Google.GoogleGeocoder(apiKey);
        }

        /// <summary>
        /// Gets the lat/long coordinates for the given address.
        /// </summary>
        /// <returns>The lat/long coordinates for address.</returns>
        /// <param name="address">The address.</param>
        public async Task<Coordinates> GetCoordinatesForAddress(string address)
        {
            var results = await _client.GeocodeAsync(address);
            var top = results.FirstOrDefault();

            if (top == null)
                return new Coordinates();

            return new Coordinates(top.Coordinates.Latitude, top.Coordinates.Longitude);
        }
    }
}
