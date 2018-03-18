using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Wappa.DataAccess.Contracts;
using Wappa.Models;

namespace Wappa.DataAccess
{
    public class GeocodingProxy : IGeocoding
    {
        private IConfiguration _configuration;
        private string _apiUrl;
        private string _apiKey;
        public GeocodingProxy(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiUrl = configuration["googleApi:geocodingUrl"];
            _apiKey = configuration["googleApi:key"];
        }
        public async Task<Localizacao> BuscarCoordenadasGeograficas(string endereco)
        {
            var address = System.Net.WebUtility.UrlEncode(endereco);
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync($"{_apiUrl}?address={address}&key={_apiKey}");
                var results = Newtonsoft.Json.JsonConvert.DeserializeObject<GeocodingResult>(json);
                return results.Resultados.FirstOrDefault()?.Geolocalizacao?.Localizacao;
            }
        }
    }
}
