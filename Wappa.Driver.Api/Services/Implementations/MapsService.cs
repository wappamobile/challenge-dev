using Geocoding;
using Geocoding.Google;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Wappa.Driver.Api.Dtos;
using Wappa.Driver.Api.Services.Interfaces;

namespace Wappa.Driver.Api.Services.Implementations
{
    public class MapsService : IMapsService
    {
        #region Private fields
        private readonly AppSettings _settings;
        #endregion

        #region Constructor
        public MapsService(AppSettings settings)
        {
            _settings = settings;
        }
        #endregion

        /// <summary>
        /// Retorna a latitude e longitude de um endereço
        /// </summary>
        /// <param name="address">Endereço do motorista</param>
        /// <returns></returns>
        public async Task<MapDto> GetGeometry(DriverAddressDto addressDto)
        {
            string address = addressDto.Address + " " + addressDto.Number + " " + addressDto.Neighborhood + " " + 
                addressDto.City + " " + addressDto.State + " " + addressDto.Country;
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = _settings.MapKey };
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(address);

            if (address.Length > 0)
            {
                return new MapDto
                {
                    Latitude = addresses.First().Coordinates.Latitude.ToString(),
                    Longitude = addresses.First().Coordinates.Longitude.ToString()
                };
            }
            return null;
        }
    }
}
