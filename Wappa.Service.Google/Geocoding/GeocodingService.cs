using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Framework.Google.Geocoding;
using Wappa.Framework.Google.Geocoding.Core;
using Wappa.Framework.Model.Comum;
using System.Linq;

namespace Wappa.Service.Geocoder
{
    public class GeocodingService : IGeocodingService
    {
        private string apiKey;

        public GeocodingService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<Endereco> ObterLocalizacaoAsync(Endereco endereco)
        {
            IGeocoderService geocoder = new GeocoderService() { ApiKey = this.apiKey };
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(endereco.Rua, endereco.Cidade, endereco.UF, endereco.CEP, endereco.Pais);

            if(addresses.Any())
            {
                Address address = addresses.First();
                endereco.Longitude = address.Coordinates.Longitude;
                endereco.Latitude = address.Coordinates.Latitude;
            }

            return endereco;
        }
    }
}
