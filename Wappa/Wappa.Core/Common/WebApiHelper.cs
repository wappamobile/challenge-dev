using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Wappa.Core.Common
{
    public static class WebApiHelper
    {
        public static HttpClient WebApiClient(string baseUri)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
