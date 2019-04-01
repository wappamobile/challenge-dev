using Wappa.Middleware.Core.GoogleMaps.VO;
using Wappa.Middleware.Core.GoogleMaps.VO.Configuration;
using Wappa.Middleware.Domain.Configuration;
using Wappa.Middleware.Domain.Drivers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.Middleware.Core.GoogleMaps
{
    public class GoogleMapManager : WappaMiddlewareServiceBase, IGoogleMapManager
    {
        public GoogleMapManager(IOptionsMonitor<GoogleMapsConfiguration> options)
            :base(options)
        {
        }

        public async Task<GoogleGeocodeOutputVO> GeocodeAdress(Driver driver)
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var addressVO = new
                {
                    Address = driver.Address + "," + driver.Number.ToString() + " - " + driver.District + " - " + driver.City,
                    Sensor = "false",
                    _options.CurrentValue.Key
                };

                UriBuilder builder = new UriBuilder(BuildUri(_options.CurrentValue.BaseUrl, _options.CurrentValue.ApiUrlAddress));
                builder.Query = string.Format("address={0}&key={1}",addressVO.Address,addressVO.Key);


                HttpResponseMessage response = client.GetAsync(builder.Uri).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<BusResultWrapper<GoogleGeocodeOutputVO>>();
                    throw new Exception(error.Error.Message);
                }
                else
                {
                    var output = JsonConvert.DeserializeObject<GoogleGeocodeOutputVO>(response.Content.ReadAsStringAsync().Result);
                    return output;
                }

               
            }
        }

        private string BuildUri(string baseUri, string uri)
        {
            if (baseUri.EndsWith("/"))
                return baseUri + uri;
            else
                return baseUri + "/" + uri;
        }
    }
}
