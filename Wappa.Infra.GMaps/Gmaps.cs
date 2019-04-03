using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Wappa.Domain.Interfaces;
using Wappa.Domain.Models;

namespace Wappa.Infra.GMaps
{
    public class Gmaps : IGMaps
    {
        private readonly Geocoding.Google.GoogleGeocoder _clientGMaps;

        public Gmaps(string apiKey)
        {
            _clientGMaps = new Geocoding.Google.GoogleGeocoder(apiKey);
        }


        public async Task<ValueObjectsGMaps> GetCoordinatesAsync(string address)
        {
            try
            {
                var resultsGMaps = await _clientGMaps.GeocodeAsync(address);
                var coordinates = resultsGMaps.FirstOrDefault();

                return new ValueObjectsGMaps()
                {
                    Coordinates = coordinates.Coordinates.Latitude + ", "
                    + coordinates.Coordinates.Longitude
                };

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
