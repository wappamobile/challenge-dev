using RestSharp;
using System;
using System.Linq;
using Wappa.Challenge.Dev.GoogleMaps.Services.Impl.Configuration;
using Wappa.Challenge.Dev.GoogleMaps.Services.Impl.Model;
using Wappa.Challenge.Dev.Models;
using Wappa.Challenge.Dev.Services;

namespace Wappa.Challenge.Dev.GoogleMaps.Services.Impl
{
    public class GoogleMapsGeoCoordenadaService : IGeoCoordenadaService
    {
        private GoogleMapsConfiguration _configuration;

        public GoogleMapsGeoCoordenadaService(GoogleMapsConfiguration googleMapsConfiguration)
        {
            _configuration = googleMapsConfiguration;
        }

        public (decimal? Latitude, decimal? Longitude) ObterGeoCoordenada(Endereco endereco)
        {
            string enderecoConsulta = GerarEnderecoConsulta(endereco);

            var restClient = new RestClient(_configuration.BaseUrl);

            var restRequest = new RestRequest(_configuration.Resources["GeoCode"]);
            restRequest.AddUrlSegment("apiKey", _configuration.ApiKey);
            restRequest.AddUrlSegment("address", enderecoConsulta);
            try
            {
                var geoCodeResult = restClient.Execute<GeoCodeResult>(restRequest);
                var location = geoCodeResult?.Data?.Results?.FirstOrDefault()?.Geometry?.Location;
                if (location != null)
                {
                    return (location.Lat, location.Lng);
                }
            }
            catch { }

            return (null, null);
        }

        private string GerarEnderecoConsulta(Endereco endereco)
        {
            return $"{endereco.TipoLogradouro} {endereco.Logradouro}, {endereco.Numero} - {endereco.Bairro} - {endereco.Cidade} - {endereco.UF}";
        }
    }
}
