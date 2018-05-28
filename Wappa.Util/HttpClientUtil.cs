using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.Util
{
    public class HttpClientUtil<T> : IDisposable where T : class
    {
        private HttpClient _client;

        public HttpClientUtil()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.Timeout = new TimeSpan(0, 10, 0);
        }



        public async Task<T> Get(string api, string parameter)
        {
            T retorno = null;

            var urlConcat = $"{api}/{parameter}";

            var response = await _client.GetAsync(urlConcat);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string resultado = await response.Content.ReadAsStringAsync();

               return JsonConvert.DeserializeObject<T>(resultado);
            }

            return retorno;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
