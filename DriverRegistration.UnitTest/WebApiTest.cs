using DriverRegistration.API;
using DriverRegistration.Application.DTOs.Address;
using DriverRegistration.Application.DTOs.Car;
using DriverRegistration.Application.DTOs.Driver;
using DriverRegistration.UnitTest.Mock;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DriverRegistration.UnitTest
{
    public class WebApiTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public IConfigurationRoot Configuration { get; }

        public WebApiTest()
        {
            _server = new TestServer(new WebHostBuilder()
           .UseStartup<Startup>()
           .ConfigureAppConfiguration((hostContext, config) =>
           {
               config.Sources.Clear();
               config.AddJsonFile("appsettings.json", optional: true);
           }));

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task AddDriver()
        {
            DriverPostRequest requestDriver = MockDriverPostRequest.Get("Teste 015", "Teste 015");

            string postData = JsonConvert.SerializeObject(requestDriver);


            var response = await _client.PostAsync("/api/driver", new StringContent(postData, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            DriverResponse _obj = JsonConvert.DeserializeObject<DriverResponse>(responseString);

            Assert.NotEqual(0, _obj.Id);
        }

        [Fact]
        public async Task AddCar()
        {
            DriverPostRequest requestDriver = MockDriverPostRequest.Get("Teste 015", "Teste 015");

            string postData = JsonConvert.SerializeObject(requestDriver);

            var response = await _client.PostAsync("/api/driver", new StringContent(postData, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            DriverResponse _obj = JsonConvert.DeserializeObject<DriverResponse>(responseString);

            CarPostRequest request = new CarPostRequest()
            {
                Brand = "Fiat",
                DriverId = _obj.Id,
                Model = "Palio",
                Plate = "hhc-7849"
            };

            postData = JsonConvert.SerializeObject(request);

            response = await _client.PostAsync("/api/car", new StringContent(postData, Encoding.UTF8, "application/json"));

            responseString = await response.Content.ReadAsStringAsync();

            CarResponse responseCar = JsonConvert.DeserializeObject<CarResponse>(responseString);

            Assert.NotEqual(0, responseCar.Id);
        }

        [Fact]
        public async Task AddAddress()
        {
            DriverPostRequest requestDriver = MockDriverPostRequest.Get("Teste 015", "Teste 015");

            string postData = JsonConvert.SerializeObject(requestDriver);

            var response = await _client.PostAsync("/api/driver", new StringContent(postData, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            DriverResponse _obj = JsonConvert.DeserializeObject<DriverResponse>(responseString);

            AddressPostRequest requestAddress = new AddressPostRequest()
            {
                AddressName = "Rua Waldir Carrião Soares"
                , City = "Mogi das Cruzes"
                , DriverId = _obj.Id
                , Neighborhood = "Vila Caputera"
                , Number = 114
                , PostalCode = "08720-400"
                , State = "SP"
            };

            postData = JsonConvert.SerializeObject(requestAddress);

            response = await _client.PostAsync("/api/address", new StringContent(postData, Encoding.UTF8, "application/json"));

            responseString = await response.Content.ReadAsStringAsync();

            AddressResponse responseAddress = JsonConvert.DeserializeObject<AddressResponse>(responseString);

            Assert.NotEqual(0, responseAddress.Id);
        }

        [Fact]
        public async Task ValidateReturnError400()
        {
            DriverPostRequest requestDriver = MockDriverPostRequest.Get("", "");

            string postData = JsonConvert.SerializeObject(requestDriver);


            var response = await _client.PostAsync("/api/driver", new StringContent(postData, Encoding.UTF8, "application/json"));

            Assert.Equal(400, (int)response.StatusCode);
        }
    }
}
