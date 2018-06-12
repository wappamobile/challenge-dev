using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Wappa.Challenge.Util.Google
{
    public class Coordenadas
    {
        /// <summary>
        /// Retorna as coordenadas do endereço. Retorna KeyValuePair<Latitude-Longitude>
        /// </summary>
        /// <param name="endereco">Logradouro, Número, Bairro, Cidade, Estado, CEP</param>
        /// <returns></returns>
        public static KeyValuePair<string, string> ObterPorEndereco(string endereco)
        {
            var googleUrl = $"http://maps.googleapis.com/maps/api/geocode/json?address={endereco}&sensor=true_or_false";
            WebClient wc = new WebClient();
            dynamic json = ((dynamic)JsonConvert.DeserializeObject(wc.DownloadString(googleUrl)));

            return new KeyValuePair<string, string>(json.results[0].geometry.location.lat.Value.ToString(), json.results[0].geometry.location.lng.Value.ToString());
        }
    }
}
