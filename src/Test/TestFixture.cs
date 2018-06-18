using Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using WebApi;
using Xunit;

namespace Test
{
    public class TestFixture : IDisposable 
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }
        public readonly Context Context;

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            _server = new TestServer(builder);
            Context = _server.Host.Services.GetService(typeof(Context)) as Context;

            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
            Context.Dispose();
        }
    }
}
