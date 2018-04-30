using Microsoft.Extensions.Configuration;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Services;
using Wappa.Challenge.Shared.Configuration;
using Xunit;

namespace Wappa.Challenge.UnitTest
{
    public class GoogleMapsTests
    {
        [Fact]
        public void GetCordinates()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("GoogleMapsConfiguration")
                .Get<GoogleMapsConfiguration>();

            var address = new AddressCommand()
            {
                Country = "Brazil",
                State = "São Paulo",
                City = "Diadema",
                Street = "Av Fabio Eduardo Ramos Esquivel",
                Number = "2900",
                Complement = "Torre 7 AP 145",
                ZipCode = "09941-202"
            };

            var googleServices = new GoogleService(config);

            var result = googleServices.GetCoordinates(address);

            Assert.NotEqual(0, result.latitude);
            Assert.NotEqual(0, result.longitude);
        }
    }
}

