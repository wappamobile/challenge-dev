using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wappa.ApiClient;
using Wappa.Models.Geolocalizacao;
using Wappa.Models.Motorista;

namespace Wappa.Services.V1 {
    public class GeolocalizacaoService : IGeolocalizacaoService {

        private IApiClientService apiClient { get; set; }
        private IConfiguration configuration { get; set; }
        public GeolocalizacaoService (IApiClientService apiClient, IConfiguration configuration) {
            this.apiClient = apiClient;
            this.configuration = configuration;
        }

        public async Task<Rootobject> ObterGeolocalizacao (MotoristaModel model) {

            var key = configuration["Google:ApiKey"];
            var url = configuration["Google:Url"];
            var urlFormatada = $"{url}{model.ToString()}&key={key}";

            var Result = await apiClient.GetAsync (urlFormatada);

            return JsonConvert.DeserializeObject<Rootobject> (await Result.Content.ReadAsStringAsync ());
        }
    }
}