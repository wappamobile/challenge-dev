using Flurl.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Wappa.Service.GeometryService

{
    public class GeometryServiceAsync : IGeometryServiceAsync
    {
        private readonly string _endpoint;
        private readonly string _apiKey;

        public GeometryServiceAsync(string endpoint, string apiKey)
        {
            _endpoint = endpoint;
            _apiKey = apiKey;
        }
        
        public async Task<Geometry> GetGeometryAsync(string endereco)
        {
            try
            {
                var responseJson = await callAPI(endereco);

                var response = parse(responseJson);

                string latitude = response.results[0].geometry.location.lat;
                string longitude = response.results[0].geometry.location.lng;

                return new Geometry(latitude, longitude);
            }
            catch (Exception ex)
            {
                return new Geometry(false);
            }           
        }

        public string ObterEnderecoCompleto(string logradouro, string numero, string bairro, string cidade, string estado, string cep)
        {
            return string.Join(" - ", new[] { logradouro, numero, bairro, cidade, estado, cep });
        }

        private async Task<string> callAPI(string address)
        {
            return await getFullUrl(address).GetStringAsync();
        }

        private string getFullUrl(string address)
        {
            return string.Format("{0}?key={1}&address={2}", _endpoint, _apiKey, address);
        }
                
        private dynamic parse(string responseString)
        {
            dynamic response = JObject.Parse(responseString);

            if (response.status != "OK") throw new InvalidOperationException();
            return response;
        }

       
    }
}
