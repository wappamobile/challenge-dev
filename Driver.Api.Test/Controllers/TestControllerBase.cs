using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Api.Test.Controllers
{
    public abstract class TestControllerBase
    {
        public HttpClient Client => TestClientProvider.Current.Client;

        public async Task<T> GetAsync<T>(string uri)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                var result = await Client.SendAsync(message);

                result.EnsureSuccessStatusCode();

                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                return await Client.SendAsync(message);
            }
        }

        public async Task<T> PostAsync<T>(string uri, object body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, uri))
            using (StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                message.Content = content;

                var result = await Client.SendAsync(message);

                result.EnsureSuccessStatusCode();

                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, object body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, uri))
            using (StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                message.Content = content;

                return await Client.SendAsync(message);
            }
        }

        public async Task<T> PutAsync<T>(string uri, object body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, uri))
            using (StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                message.Content = content;

                var result = await Client.SendAsync(message);

                result.EnsureSuccessStatusCode();

                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, object body)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, uri))
            using (StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"))
            {
                message.Content = content;

                return await Client.SendAsync(message);
            }
        }


        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, uri))
            {
                return await Client.SendAsync(message);
            }
        }
    }
}
