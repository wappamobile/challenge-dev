using Driver.Api.ViewModels;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Driver.Api.Test.Controllers
{
    public class DriverControllerTest : TestControllerBase
    {
        public const string Url = "/api/Driver/";

        [Fact]
        public async void Get_Success()
        {
            var result = await GetAsync<List<GetDriverViewModel>>(Url);

            Assert.NotEmpty(result);
            Assert.Contains(result, i => i.DriverId == 1);
        }

        [Fact]
        public async void GetById_Success()
        {
            var result = await GetAsync<GetDriverByIdViewModel>(Url + "1");

            Assert.NotNull(result);
            Assert.Equal(1, result.DriverId);
            Assert.Equal("Usuário", result.FirstName);
            Assert.NotNull(result.Address);
            Assert.NotNull(result.Car);
        }

        [Fact]
        public async void GetById_Fail_NotFound()
        {
            var result = await GetAsync(Url + "0");

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async void Post_Success()
        {
            var result = await PostAsync<GetDriverByIdViewModel>(Url, new PostDriverViewModel
            {
                Address = new PostAddressViewModel
                {
                    Address = "Rua Ramos Batista",
                    City = "São Paulo",
                    District = "Vila Olimpia",
                    Number = "198",
                    State = "SP",
                    ZipCode = "04552020"
                },
                Car = new CarViewModel
                {
                    Brand = "Volkswagen",
                    Model = "Gol",
                    LicensePlate = "BBB2222"
                },
                FirstName = "Usuário",
                LastName = "Incluido Teste"
            });

            Assert.NotNull(result);
            Assert.NotEqual(0, result.DriverId);
            Assert.Equal("Usuário", result.FirstName);
            Assert.Equal("Incluido Teste", result.LastName);
            Assert.NotNull(result.Address);

            Assert.Equal("Rua Ramos Batista", result.Address.Address);
            Assert.Equal("São Paulo", result.Address.City);
            Assert.Equal("Vila Olimpia", result.Address.District);
            Assert.Equal("198", result.Address.Number);
            Assert.Equal("SP", result.Address.State);
            Assert.Equal("04552020", result.Address.ZipCode);

            Assert.NotNull(result.Car);

            Assert.Equal("Volkswagen", result.Car.Brand);
            Assert.Equal("Gol", result.Car.Model);
            Assert.Equal("BBB2222", result.Car.LicensePlate);
        }

        [Fact]
        public async void Post_Fail_BadRequest()
        {
            var result = await PostAsync(Url, new PostDriverViewModel());

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void Put_Success()
        {
            var result = await PutAsync<GetDriverByIdViewModel>(Url + "1", new PutDriverViewModel
            {
                Address = new PostAddressViewModel
                {
                    Address = "Rua Ramos Batista",
                    City = "São Paulo",
                    District = "Vila Olimpia",
                    Number = "198",
                    State = "SP",
                    ZipCode = "04552020"
                },
                Car = new CarViewModel
                {
                    Brand = "Volkswagen",
                    Model = "Gol",
                    LicensePlate = "AAA1111"
                },
                FirstName = "Usuário",
                LastName = "Atualizado Teste"
            });

            Assert.NotNull(result);
            Assert.Equal(1, result.DriverId);
            Assert.Equal("Usuário", result.FirstName);
            Assert.Equal("Atualizado Teste", result.LastName);
            Assert.NotNull(result.Address);

            Assert.Equal("Rua Ramos Batista", result.Address.Address);
            Assert.Equal("São Paulo", result.Address.City);
            Assert.Equal("Vila Olimpia", result.Address.District);
            Assert.Equal("198", result.Address.Number);
            Assert.Equal("SP", result.Address.State);
            Assert.Equal("04552020", result.Address.ZipCode);

            Assert.NotNull(result.Car);

            Assert.Equal("Volkswagen", result.Car.Brand);
            Assert.Equal("Gol", result.Car.Model);
            Assert.Equal("AAA1111", result.Car.LicensePlate);
        }

        [Fact]
        public async void Put_Fail_BadRequest()
        {
            var result = await PutAsync(Url + "1", new PutDriverViewModel());

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async void Put_Fail_NotFound()
        {
            var result = await PutAsync(Url + "0", new PutDriverViewModel
            {
                Address = new PostAddressViewModel
                {
                    Address = "Rua Ramos Batista",
                    City = "São Paulo",
                    District = "Vila Olimpia",
                    Number = "198",
                    State = "SP",
                    ZipCode = "04552020"
                },
                Car = new CarViewModel
                {
                    Brand = "Volkswagen",
                    Model = "Gol",
                    LicensePlate = "AAA1111"
                },
                FirstName = "Usuário",
                LastName = "Atualizado Teste"
            });

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async void Delete_Success()
        {
            var result = await DeleteAsync(Url + "2");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void Delete_Fail_NotFound()
        {
            var result = await DeleteAsync(Url + "0");

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}