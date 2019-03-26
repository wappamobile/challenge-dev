using System;
using Cadastro.Entities;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace Cadastro.Api
{
    public class ConsultaApiGoogle
    {

        private string _apiKey;

        public Endereco Consulta(string endereco)
        {
            var cliente = new RestClient("https://maps.googleapis.com/");
            var chamada = new RestRequest("maps/api/geocode/json");
            chamada.AddParameter("address", endereco);
            chamada.AddParameter("key", _apiKey);


            var result = cliente.Execute(chamada);
            if (!result.IsSuccessful)
            {
                throw new ApplicationException(result.ErrorMessage);
            }

            var resposta = JObject.Parse(result.Content);

            return new Endereco
            {
                Logradouro = resposta.SelectToken("results[0].formatted_address").Value<string>(),
                Coordenada =
                    new Coordenada
                    {
                        Longitude = resposta.SelectToken("results[0].geometry.location.lng").Value<string>(),
                        Latitude = resposta.SelectToken("results[0].geometry.location.lat").Value<string>()

                    },

            };

        }

    }
}
