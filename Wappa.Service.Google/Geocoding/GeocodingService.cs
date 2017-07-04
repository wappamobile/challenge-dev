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
        public async Task<Endereco> ObterLocalizacaoAsync()
        {
            IGeocoderService geocoder = new GeocoderService() { ApiKey = "AIzaSyBsMg2pHj_HKH8SiMYQ3gvbCkobUotdKwg" };
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync("Avenida Bosque da Saude 710 Sao Paulo SP Brasil");

            return new Endereco();
        }
    }
}
