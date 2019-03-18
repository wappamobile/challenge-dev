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
    public class DriverCatalogControllerCRUDScenarios : IDisposable
    {
        private TestEntryPoint _lambdaFunction;

        public DriverCatalogControllerCRUDScenarios()
        {
            TestEntryPoint.RepositoryTableName = "DriverCatalog-" + DateTime.Now.Ticks;
        }

        [Fact]
        public async Task Creating_a_driver_with_valid_data()
        {
            // ARRANGE
            var newDriver = new Driver { FirstName = "Humberto", LastName = "Bulhões" };
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            // ACT
            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(201, response.StatusCode);
            Assert.True(response.MultiValueHeaders.ContainsKey("Location"));
            Assert.True(!string.IsNullOrEmpty(response.Body));

            var newDriverId = response.Body;

            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Get.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Path = string.Format(request.Path, newDriverId);
            request.PathParameters["proxy"] = string.Format(request.PathParameters["proxy"], newDriverId);
            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            Assert.Equal(200, response.StatusCode);
            var returnedDriver = JsonConvert.DeserializeObject<Driver>(response.Body);
            Assert.Equal(newDriver.FirstName, returnedDriver.FirstName);
            Assert.Equal(newDriver.LastName, returnedDriver.LastName);
        }
        
        [Fact]
        public async Task Trying_to_create_a_driver_with_invalid_data()
        {
            // ARRANGE
            var newDriver = new Driver { FirstName = "", LastName = null };
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            // ACT
            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(400, response.StatusCode);

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Body);
            Assert.Contains(errorResponse.Errors, e => e.Where == nameof(Driver.FirstName) && e.Problem == "Cannot be empty");
            Assert.Contains(errorResponse.Errors, e => e.Where == nameof(Driver.LastName) && e.Problem == "Cannot be empty");
        }
        
        [Fact]
        public async Task Trying_to_create_a_driver_that_already_exists()
        {
            // ARRANGE
            var newDriver = new Driver { FirstName = "Humberto", LastName = "Bulhões" };
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);
            Assert.Equal(201, response.StatusCode);

            // ACT
            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context); // replays the previous request

            // ASSERT
            Assert.Equal(400, response.StatusCode);

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Body);
            Assert.Contains(errorResponse.Errors, e => e.Where == nameof(Driver) && e.Problem == "Already exists");
        }

        [Fact]
        public async Task Updating_a_driver_with_valid_data()
        {
            // ARRANGE
            var newDriver = new Driver { FirstName = "Humberto", LastName = "Bulhoes" };
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ACT
            var updatedDriver = new Driver
            {
                Id = response.Body,
                FirstName = newDriver.FirstName,
                LastName = "Bulhões"
            };

            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Put.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(updatedDriver);

            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(204, response.StatusCode);

            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Get.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Path = string.Format(request.Path, updatedDriver.Id);
            request.PathParameters["proxy"] = string.Format(request.PathParameters["proxy"], updatedDriver.Id);
            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            Assert.Equal(200, response.StatusCode);
            var returnedDriver = JsonConvert.DeserializeObject<Driver>(response.Body);
            Assert.Equal(updatedDriver.FirstName, returnedDriver.FirstName);
            Assert.Equal(updatedDriver.LastName, returnedDriver.LastName);
            Assert.True(returnedDriver.ModifiedAt > returnedDriver.CreatedAt);
        }

        [Fact]
        public async Task Trying_to_update_of_a_driver_with_invalid_data()
        {
            // ARRANGE
            var newDriver = new Driver { FirstName = "Humberto", LastName = "Bulhoes" };
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ACT
            var updatedDriver = new Driver
            {
                Id = response.Body,
                FirstName = newDriver.FirstName,
                LastName = null
            };

            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Put.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(updatedDriver);

            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(400, response.StatusCode);

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Body);
            Assert.Contains(errorResponse.Errors, e => e.Where == nameof(Driver.LastName) && e.Problem == "Cannot be empty");
        }

        [Fact]
        public async Task Trying_to_update_a_driver_that_does_not_exist()
        {
            // ARRANGE
            var recordToUpdate = new Driver { Id = new Guid().ToString(), FirstName = "Humberto", LastName = "Bulhoes" };
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Put.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(recordToUpdate);

            // ACT
            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(404, response.StatusCode);

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Body);
            Assert.Contains(errorResponse.Errors, e => e.Where == nameof(Driver) && e.Problem == "Not found");
        }

        [Fact]
        public async Task Deleting_a_driver()
        {
            // ARRANGE
            var newDriver = new Driver { FirstName = "Humberto", LastName = "Bulhoes" };
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);
            var driverId = response.Body;

            // ACT
            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Delete.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Path = string.Format(request.Path, driverId);
            request.PathParameters["proxy"] = string.Format(request.PathParameters["proxy"], driverId);

            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(204, response.StatusCode);

            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Get.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            
            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task Trying_to_delete_a_driver_that_does_not_exist()
        {
            // ARRANGE
            var inexistentDriverId = new Guid().ToString();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Delete.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Path = string.Format(request.Path, inexistentDriverId);
            request.PathParameters["proxy"] = string.Format(request.PathParameters["proxy"], inexistentDriverId);

            // ACT
            _lambdaFunction = new TestEntryPoint();
            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(404, response.StatusCode);

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Body);
            Assert.Contains(errorResponse.Errors, e => e.Where == nameof(Driver) && e.Problem == "Not found");
        }

        public void Dispose()
        {
            _lambdaFunction?.Dispose();
        }
    }
}