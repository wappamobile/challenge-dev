using Newtonsoft.Json;
using System;
using System.Xml;
using Teste.Servicos.Externos.DTO;
using Teste.Servicos.Externos.GoogleResponse;

namespace Teste.Servicos.Externos
{
    
    public class ServicoGoogleMaps : IServicoGoogleMaps
    {
        public Coordenadas ObterCoordenadas(string endereco)
        {
            var googleMapsURL = string.Format(ObterURLServico(), endereco);
            var result = new System.Net.WebClient().DownloadString(googleMapsURL);
            
            var googleResponse = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);

            if (googleResponse.results.Length == 0) throw new Exception("Não foram encontradas as coordenadas para o endereço informado.");

            var firstLocation = googleResponse.results[0].geometry.location;
            var latitude = Convert.ToDouble(firstLocation.lat.Replace(".",","));
            var longitude = Convert.ToDouble(firstLocation.lng.Replace(".", ","));

            return new Coordenadas(latitude, longitude);
        }

        private string ObterURLServico() {
            return @"http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false";
        }
    }
}
