using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using DriverCatalogService.Models;
using Newtonsoft.Json;
using Xunit;

namespace DriverCatalogService.Tests
{
    public class DriverCatalogQueryScenarios : IDisposable
    {
        private TestEntryPoint _lambdaFunction;

        public DriverCatalogQueryScenarios()
        {
            TestEntryPoint.RepositoryTableName = "DriverCatalog-QueryTests-" + DateTime.Now.Ticks;
        }

        [Fact]
        public async Task Listing_all_drivers_sorting_by_first_name()
        {
            // ARRANGE
            _lambdaFunction = new TestEntryPoint();

            await CreateDriver(new Driver { FirstName = "Humberto", LastName = "Bulhões" });
            await CreateDriver(new Driver { FirstName = "José", LastName = "Alves" });

            // ACT
            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-ListByFirstName.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(200, response.StatusCode);

            var driverList = JsonConvert.DeserializeObject<Driver[]>(response.Body);
            Assert.NotEmpty(driverList);
            Assert.Equal("Humberto", driverList[0].FirstName);
            Assert.Equal("José", driverList[1].FirstName);
        }

        [Fact]
        public async Task Listing_all_drivers_sorting_by_last_name()
        {
            // ARRANGE
            _lambdaFunction = new TestEntryPoint();

            await CreateDriver(new Driver { FirstName = "Humberto", LastName = "Bulhões" });
            await CreateDriver(new Driver { FirstName = "José", LastName = "Alves" });

            // ACT
            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-ListByLastName.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(200, response.StatusCode);

            var driverList = JsonConvert.DeserializeObject<Driver[]>(response.Body);
            Assert.NotEmpty(driverList);
            Assert.Equal("Alves", driverList[0].LastName);
            Assert.Equal("Bulhões", driverList[1].LastName);
        }

        private async Task CreateDriver(Driver newDriver)
        {
            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            var context = new TestLambdaContext();
            await _lambdaFunction.FunctionHandlerAsync(request, context);
        }

        public void Dispose()
        {
            _lambdaFunction?.Dispose();
        }
    }
}