using System;
using DriverCatalogService.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace DriverCatalogService.Tests
{
    public class TestEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction, IDisposable
    {
        public static string RepositoryTableName;
        private IRepository _repository;

        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseSetting("Repository:TableName", RepositoryTableName);
            builder.UseStartup<TestStartup>();
        }

        protected override void PostCreateWebHost(IWebHost webHost)
        {
            _repository = webHost.Services.GetService(typeof(IRepository)) as IRepository;
            _repository?.SetupTable().Wait();
        }

        public void Dispose()
        {
            _repository?.DropTable();
        }
    }
}