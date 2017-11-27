using System.Text;
using System.Net.Http;
using WappaChallenge.DTO;
using WappaChallenge.AppServices.Adapters;
using System.Threading.Tasks;

namespace WappaChallenge.AppServices.Facade
{
    public class GoogleMapsAPIFacade
    {
        private readonly string _urlGoogleAPI;

        public GoogleMapsAPIFacade()
        {
            _urlGoogleAPI = "";
        }

        public async Task<CoordenadaGeograficaDTO> ObterCoordenadasGeograficas(EnderecoDTO dto)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent conteudo = new StringContent(dto.ParaJson(), Encoding.UTF8, "application/json");
                var resposta = await client.PostAsync(this._urlGoogleAPI, conteudo);

                return new CoordenadaGeograficaDTO();
            }
        }
    }
}
