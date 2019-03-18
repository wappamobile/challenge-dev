using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;


namespace Wappa.Driver.Tests.Fixtures
{
    public static class TestServerFixture
    {
        private static readonly TestServer _server;
        private static readonly HttpClient _client;

        static TestServerFixture()
        {
            _server = new TestServer(new WebHostBuilder().UseEnvironment("Test").UseStartup<StartupTest>());
            _client = _server.CreateClient();
        }

        public static TestServer Server => _server;

        public static IFlurlClient Client =>
            new FlurlClient(_server.BaseAddress.ToString()).Configure(x =>
                x.HttpClientFactory = new TestHttpClientFactory(_client));
    }
}
