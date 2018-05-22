using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wappa.Core.Interfaces;
using Wappa.Core.Models;

namespace Wappa.Infra.Repositories
{
    public class GoogleMapsCoordenadasRepository : ICoordenadasRepository
    {
        private readonly string _baseUrl;

        private readonly string _googleMapsKey;

        public GoogleMapsCoordenadasRepository(string baseUrl, string googleMapsKey)
        {
            _baseUrl = baseUrl;
            _googleMapsKey = googleMapsKey;
        }

        public async Task<CoordenadasData> Get(Endereco endereco)
        {
            CoordenadasData coordenadas = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(
                string.Format("{0}?address={1}+{2}&components=locality:{3}&key={4}"
                    , _baseUrl
                    , endereco.Logradouro
                    , endereco.Numero
                    , endereco.Cidade
                    , _googleMapsKey));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                coordenadas = JsonConvert.DeserializeObject<CoordenadasData>(result);
                return coordenadas;
            }

            return null;            
        }
    }
}