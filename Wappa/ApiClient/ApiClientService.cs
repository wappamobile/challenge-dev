using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wappa.ApiClient {

    public class ApiClientService : IApiClientService {

        private HttpClient client;

        public ApiClientService () {
            this.client = new HttpClient ();
        }

        public async Task<HttpResponseMessage> GetAsync (string requestUrl) {
            var message = new HttpRequestMessage (HttpMethod.Get, requestUrl);

            return await SendAsync (message, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> SendAsync (HttpRequestMessage message, CancellationToken cancellationToken) {
            var response = await this.client.SendAsync (message, cancellationToken);

            return response;
        }
    }

    public interface IApiClientService {
        Task<HttpResponseMessage> GetAsync (string requestUrl);
        Task<HttpResponseMessage> SendAsync (HttpRequestMessage message, CancellationToken cancellationToken);
    }
}