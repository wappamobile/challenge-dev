using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wappa.DataAccess.Interfaces;
using Wappa.Models;

namespace Wappa.DataAccess.Implementations
{
    public class GoogleMapsClient : IGoogleMapsClient
    {
        private readonly IConfiguration configuration;
        private readonly string urlApi;
        private readonly string tokenApi;

        public GoogleMapsClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.urlApi = configuration["GoogleMapsAPI:URLApi"];
            this.tokenApi = configuration["GoogleMapsAPI:KeyApi"];
        }

        public async Task<Localizacao> ObterCoordenadas(Endereco endereco)
        {
            var enderecoCompleto = System.Net.WebUtility.UrlEncode($"{endereco.Rua},{endereco.Numero},{endereco.Cidade},{endereco.UF}");
            using (var client = new HttpClient())
            {
                var jsonResult = await client.GetStringAsync($"{urlApi}?address={enderecoCompleto}&key={tokenApi}");
                var coordenadas = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(jsonResult);

                return coordenadas.Resultados.FirstOrDefault().Coordenadas.Localizacao;
            }
        }
    }
}
