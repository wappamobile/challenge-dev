using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WappaMobile.ChallengeDev.Models;

namespace WappaMobile.ChallengeDev.GoogleMaps
{
    public class GeoCodeServices
    {
        public async Task<Coordenadas> BuscarAsync(Endereco endereco)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Settings.URL_BASE);
                var resultado = await client.GetAsync($@"json?address={endereco}&key={Settings.API_KEY}");

                if(resultado.IsSuccessStatusCode)
                {
                    try
                    {
                        var e = JsonConvert.DeserializeObject<GeoCode>(await resultado.Content.ReadAsStringAsync());
                        return new Coordenadas(e.lat, e.lng);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }

            return new Coordenadas();
        }
    }
}
