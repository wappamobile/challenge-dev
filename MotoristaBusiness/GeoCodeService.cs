using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MotoristaEntity;
using Newtonsoft.Json;

namespace MotoristaBusiness
{
    public class GeoCodeService : IGeoCodeService
    {
        public GeoCodeService()
        {
        }

        public async Task<GeoCode> GetGeoCode(string endereco)
        {
            GeoCode geocode = null;
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("https://maps.googleapis.com/maps/api/geocode/json?address=" + endereco + "&key=AIzaSyBlnh83lhaRqE0IgJ7c2hHj5Kpwa9aB54I");
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var text = await result.Content.ReadAsStringAsync();
                    geocode = JsonConvert.DeserializeObject<GeoCode>(text);
                }
            }
            return geocode;
        }
    }
}
