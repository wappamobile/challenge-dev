using Flurl.Http.Configuration;
using System.Net.Http;
namespace Wappa.Driver.Tests
{
    public class TestHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public TestHttpClientFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return _httpClient;
        }
    }
}
