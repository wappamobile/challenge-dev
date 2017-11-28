using System.Net.Http;
using WappaChallenge.DTO;
using WappaChallenge.AppServices.Adapters;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WappaChallenge.AppServices.Facade
{
    public class GoogleMapsAPIFacade
    {
        private string _urlGoogleAPI;
        private string _keyGoogleAPI;

        public GoogleMapsAPIFacade()
        {
            _urlGoogleAPI = $"https://maps.googleapis.com/maps/api/geocode/";
            _keyGoogleAPI = @"AIzaSyCT0msTRTRXZYxeioeN5xeIbhFhwdMgui0";
        }

        public async Task<CoordenadaGeograficaDTO> ObterCoordenadasGeograficas(EnderecoDTO dto)
        {
            using (HttpClient client = new HttpClient())
            {
                this._urlGoogleAPI += $"json?address={dto.ParaPadraoGoogleMapsGeoCode()}&key={this._keyGoogleAPI}";
                var resposta = await client.GetAsync(this._urlGoogleAPI);

                var d = JsonConvert.DeserializeObject<GoogleAPIGeocodeResponseDTO>(resposta.Content.ReadAsStringAsync().Result);
                return new CoordenadaGeograficaDTO
                {
                    Latitude = d.results[0].geometry.location.lat,
                    Longitude = d.results[0].geometry.location.lng
                };
            }
        }
    }
}
