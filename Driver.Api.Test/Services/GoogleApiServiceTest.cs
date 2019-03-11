using Driver.Application.Data.Entities;
using Driver.Application.Services;
using Xunit;

namespace Driver.Api.Test.Services
{
    public class GoogleApiServiceTest
    {
        [Fact]
        public void BuildAddressQuery_Success()
        {
            var expected = "R.+Ramos+Batista%2C+198+-+Vila+Olimpia%2C+S%C3%A3o+Paulo+-+SP";

            var actual = GoogleApiService.BuildAddressQuery(new AddressEntity
            {
                Address = "R. Ramos Batista",
                AddressNumber = "198",
                City = "São Paulo",
                District = "Vila Olimpia",
                State = "SP"
            });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildAddressQuery_Success_Missing_Number()
        {
            var expected = "R.+Ramos+Batista+-+Vila+Olimpia%2C+S%C3%A3o+Paulo+-+SP";

            var actual = GoogleApiService.BuildAddressQuery(new AddressEntity
            {
                Address = "R. Ramos Batista",
                City = "São Paulo",
                District = "Vila Olimpia",
                State = "SP"
            });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildAddressQuery_Success_Missing_City()
        {
            var expected = "R.+Ramos+Batista%2C+198+-+Vila+Olimpia+-+SP";

            var actual = GoogleApiService.BuildAddressQuery(new AddressEntity
            {
                Address = "R. Ramos Batista",
                AddressNumber = "198",
                District = "Vila Olimpia",
                State = "SP"
            });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildAddressQuery_Success_Missing_State()
        {
            var expected = "R.+Ramos+Batista%2C+198+-+Vila+Olimpia%2C+S%C3%A3o+Paulo";

            var actual = GoogleApiService.BuildAddressQuery(new AddressEntity
            {
                Address = "R. Ramos Batista",
                AddressNumber = "198",
                City = "São Paulo",
                District = "Vila Olimpia"
            });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildAddressQuery_Success_Missing_District()
        {
            var expected = "R.+Ramos+Batista%2C+198+-+S%C3%A3o+Paulo+-+SP";

            var actual = GoogleApiService.BuildAddressQuery(new AddressEntity
            {
                Address = "R. Ramos Batista",
                AddressNumber = "198",
                City = "São Paulo",
                State = "SP"
            });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildAddressQuery_Success_Missing_Address()
        {
            var expected = "198+-+Vila+Olimpia%2C+S%C3%A3o+Paulo+-+SP";

            var actual = GoogleApiService.BuildAddressQuery(new AddressEntity
            {
                AddressNumber = "198",
                City = "São Paulo",
                District = "Vila Olimpia",
                State = "SP"
            });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void Search_Success()
        {
            var actual = await new GoogleApiService().SearchAsync(new AddressEntity
            {
                Address = "R. Ramos Batista",
                AddressNumber = "198",
                City = "São Paulo",
                District = "Vila Olimpia",
                State = "SP"
            });
        }
    }
}