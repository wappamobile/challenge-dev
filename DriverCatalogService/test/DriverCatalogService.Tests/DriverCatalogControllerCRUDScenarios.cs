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
        private Address _defaultAddress = new Address {FullAddress = "Av. Brg. Faria Lima, 1811"};
        private Car _defaultCar = new Car {LicensePlate = "ABC-1234", Maker = "Ford", Model = "Focus Titanium 2.0 AT"};

        public DriverCatalogControllerCRUDScenarios()
        {
            TestEntryPoint.RepositoryTableName = "DriverCatalog-CRUDTests-" + DateTime.Now.Ticks;
        }

        [Fact]
        public async Task Creating_a_driver_with_valid_data()
        {
            // ARRANGE
            var newDriver = new Driver {Name = new Name {FirstName = "Humberto", LastName = "Bulhões"}, Address = _defaultAddress, Car = _defaultCar};
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
            var returnedDriver = await GetDriverById(newDriverId);
            Assert.Equal(newDriver.Name.FirstName, returnedDriver.Name.FirstName);
            Assert.Equal(newDriver.Name.LastName, returnedDriver.Name.LastName);
        }
        
        [Fact]
        public async Task Evaluating_geo_coordinates_for_a_given_drivers_address()
        {
            // ARRANGE
            var newDriver = new Driver {Name = new Name {FirstName = "Humberto", LastName = "Bulhões"}, Address = _defaultAddress, Car = _defaultCar};
            _lambdaFunction = new TestEntryPoint();

            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Post.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(newDriver);

            // ACT
            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(201, response.StatusCode);

            var driver = await GetDriverById(response.Body);
            Assert.False(string.IsNullOrEmpty(driver.Address.Latitude));
            Assert.False(string.IsNullOrEmpty(driver.Address.Longitude));
        }

        [Fact]
        public async Task Trying_to_create_a_driver_with_invalid_data()
        {
            // ARRANGE
            var newDriver = new Driver {Name = new Name {FirstName = "", LastName = null}, Address = _defaultAddress, Car = _defaultCar};
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
            Assert.Contains(errorResponse.Errors, e => e.Where == $"{nameof(Driver.Name)}.{nameof(Driver.Name.FirstName)}" && e.Problem == "Cannot be empty");
            Assert.Contains(errorResponse.Errors, e => e.Where == $"{nameof(Driver.Name)}.{nameof(Driver.Name.LastName)}" && e.Problem == "Cannot be empty");
        }
        
        [Fact]
        public async Task Trying_to_create_a_driver_that_already_exists()
        {
            // ARRANGE
            var newDriver = new Driver { Name = new Name{ FirstName = "Humberto", LastName = "Bulhões" }, Address = _defaultAddress, Car = _defaultCar};
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
            var newDriver = new Driver {Name = new Name {FirstName = "Humberto", LastName = "Bulhoes"}, Address = _defaultAddress, Car = _defaultCar};
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
                Name = new Name
                {
                    FirstName = newDriver.Name.FirstName,
                    LastName = "Bulhões"
                }
            };

            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Put.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(updatedDriver);

            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(204, response.StatusCode);

            var returnedDriver = await GetDriverById(updatedDriver.Id);
            Assert.Equal(updatedDriver.Name.FirstName, returnedDriver.Name.FirstName);
            Assert.Equal(updatedDriver.Name.LastName, returnedDriver.Name.LastName);
            Assert.True(returnedDriver.ModifiedAt > returnedDriver.CreatedAt);
        }

        [Fact]
        public async Task Trying_to_update_of_a_driver_with_invalid_data()
        {
            // ARRANGE
            var newDriver = new Driver { Name = new Name { FirstName = "Humberto", LastName = "Bulhoes" }, Address = _defaultAddress, Car = _defaultCar};
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
                Name = new Name
                {
                    FirstName = newDriver.Name.FirstName,
                    LastName = null
                }
            };

            requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Put.json");
            request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Body = JsonConvert.SerializeObject(updatedDriver);

            context = new TestLambdaContext();
            response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            // ASSERT
            Assert.Equal(400, response.StatusCode);

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Body);
            Assert.Contains(errorResponse.Errors, e => e.Where == $"{nameof(Driver.Name)}.{nameof(Driver.Name.LastName)}" && e.Problem == "Cannot be empty");
        }

        [Fact]
        public async Task Trying_to_update_a_driver_that_does_not_exist()
        {
            // ARRANGE
            var recordToUpdate = new Driver {Id = new Guid().ToString(), Name = new Name {FirstName = "Humberto", LastName = "Bulhoes"}, Address = _defaultAddress, Car = _defaultCar};
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
            var newDriver = new Driver { Name = new Name { FirstName = "Humberto", LastName = "Bulhoes" }, Address = _defaultAddress, Car = _defaultCar};
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

            var driver = await GetDriverById(driverId);
            Assert.Null(driver);
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

        private async Task<Driver> GetDriverById(string driverId)
        {
            var requestStr = File.ReadAllText("./SampleRequests/DriverCatalogController-Get.json");
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
            request.Path = string.Format(request.Path, driverId);
            request.PathParameters["proxy"] = string.Format(request.PathParameters["proxy"], driverId);

            var context = new TestLambdaContext();
            var response = await _lambdaFunction.FunctionHandlerAsync(request, context);

            return JsonConvert.DeserializeObject<Driver>(response.Body);
        }

        public void Dispose()
        {
            _lambdaFunction?.Dispose();
        }
    }
}