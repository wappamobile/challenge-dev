using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Wappa.Driver.Api.Dtos;

namespace Wappa.Driver.Tests
{
    [TestFixture]
    public class DriverControllerTests
    {
        #region Private fields
        private readonly IFlurlClient _flurlClient;
        private const string DriverControllerTraitName = "DriverController";
        private const string PostDriverMethod = "/InsertDriver";
        private const string PutDriverMethod = "/UpdateDriver";
        private const string DeleteDriverMethod = "/DeleteDriver/?Id={0}";
        private const string GetDriverMethod = "/GetDrivers";
        #endregion

        #region Constructor
        public DriverControllerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<StartupTest>());
            var client = server.CreateClient();
            _flurlClient =
                new FlurlClient(server.BaseAddress.ToString()).Configure(c =>
                    c.HttpClientFactory = new TestHttpClientFactory(client));
        }
        #endregion

        #region Post
        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_Driver_Return400()
        {
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(null));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverCarDto_Return400()
        {
            DriverDto driver = new DriverDto{};
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverAddressDto_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto()
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverFirstName_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto(), DriverAddressDto = new DriverAddressDto()
            };

            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverLastName_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto(),
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverCarDto_Make_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto(),
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test", LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverCarDto_Model_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make"
                },
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverCarDto_Plate_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverAddressDto_Address_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverAddressDto_City_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverAddressDto_Country_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverAddressDto_Neighborhood_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverAddressDto_Number_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Null_DriverAddressDto_State_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood",
                    Number = "88"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PostDriverMethod.WithClient(_flurlClient).PostJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Post_Driver_Return200()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model",
                    Plate = "asd4521"
                    
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood",
                    State = "State",
                    Number = "88"
                },
                FirstName = "Test",
                LastName = "Last"
            };

            var result = await PostDriverMethod.WithClient(_flurlClient)
                    .PostJsonAsync(driver).ReceiveJson<int>();
            Assert.Greater(result, 0);
        }

        #endregion

        #region Put
        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_Driver_Return400()
        {
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(null));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverCarDto_Return400()
        {
            DriverDto driver = new DriverDto { };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverAddressDto_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto()
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverId_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto(),
                DriverAddressDto = new DriverAddressDto()
            };

            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverFirstName_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto(),
                DriverAddressDto = new DriverAddressDto()
            };

            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverLastName_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto(),
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverCarDto_Make_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto(),
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverCarDto_Model_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make"
                },
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverCarDto_Plate_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto(),
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverAddressDto_Address_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverAddressDto_City_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverAddressDto_Country_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverAddressDto_Neighborhood_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverAddressDto_Number_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Null_DriverAddressDto_State_Return400()
        {
            DriverDto driver = new DriverDto
            {
                DriverId = 1,
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model"
                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood",
                    State = "State"
                },
                FirstName = "Test",
                LastName = "Last"
            };
            var exception = Assert.ThrowsAsync<FlurlHttpException>(() =>
                PutDriverMethod.WithClient(_flurlClient).PutJsonAsync(driver));
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Call.HttpStatus);
        }

        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Put_Driver_Return200()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model",
                    Plate = "asd4521"

                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood",
                    State = "State",
                    Number = "88"
                },
                FirstName = "Test",
                LastName = "Last"
            };

            DriverDto driverUpdate = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model",
                    Plate = "asd4521"

                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood",
                    State = "State",
                    Number = "88"
                },
                FirstName = "Test",
                LastName = "Last"
            };

            //Insere
            var insert = await PostDriverMethod.WithClient(_flurlClient)
                    .PostJsonAsync(driver).ReceiveJson<int>();

            driverUpdate.FirstName = "update";
            driverUpdate.DriverId = insert;
            //Insere
            var update = await PutDriverMethod.WithClient(_flurlClient)
                    .PutJsonAsync(driverUpdate).ReceiveJson<DriverDto>();

            Assert.AreNotEqual(driver.FirstName, update.FirstName);
        }
        #endregion

        #region Get
        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Get_Program_Return200()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model",
                    Plate = "asd4521"

                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood",
                    State = "State",
                    Number = "88"
                },
                FirstName = "Test",
                LastName = "Last"
            };

            //Insere
            var insert = await PostDriverMethod.WithClient(_flurlClient)
                    .PostJsonAsync(driver).ReceiveJson<int>();

            //Seleciona
            var result = await GetDriverMethod.WithClient(_flurlClient)
                    .GetJsonAsync<List<DriverDto>>();
            Assert.Greater(result.Count, 0);
        }
        #endregion

        #region Delete
        [Test]
        [Category(DriverControllerTraitName)]
        public async Task Delete_Program_Return200()
        {
            DriverDto driver = new DriverDto
            {
                DriverCarDto = new DriverCarDto
                {
                    Make = "Make",
                    Model = "Model",
                    Plate = "asd4521"

                },
                DriverAddressDto = new DriverAddressDto
                {
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    Neighborhood = "Neighborhood",
                    State = "State",
                    Number = "88"
                },
                FirstName = "Test",
                LastName = "Last"
            };

            //Insere
            var insert = await PostDriverMethod.WithClient(_flurlClient)
                    .PostJsonAsync(driver).ReceiveJson<int>();

            var result = await string.Format(DeleteDriverMethod, insert).WithClient(_flurlClient)
                    .DeleteAsync();
            Assert.IsTrue(result.IsSuccessStatusCode);
        }
        #endregion
    }
}
