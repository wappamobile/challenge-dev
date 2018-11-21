using System;
using System.Net.Http;
using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Moq;
using Xunit;

namespace Domain.Test.Service
{
    public class GeocodingServiceTest
    {
        readonly Mock<IGeocodingRepository> mockGeocodingRepository = new Mock<IGeocodingRepository>();
        readonly Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
        readonly string mockedGeocodingApiKey = "AIzaSyA1k0SUXcYfM4IjeTZQSpasHk7_BU9bcU8";
        readonly Address validAddress = new Address()
        {
            City = "São Paulo",
            State = "São Paulo",
            Complement = "Casa 01",
            Country = "Brasil",
            Neighborhood = "Vila Paranaguá",
            Number = 93,
            Street = "Rua Victoria Simionato",
            ZipCode = "03808-170"
        };
        readonly string mockedJson = "{\n   \"results\" : [\n      {\n         \"address_components\" : [\n            {\n               \"long_name\" : \"93\",\n               \"short_name\" : \"93\",\n               \"types\" : [ \"street_number\" ]\n            },\n            {\n               \"long_name\" : \"Rua Victória Simionato\",\n               \"short_name\" : \"Rua Victória Simionato\",\n               \"types\" : [ \"route\" ]\n            },\n            {\n               \"long_name\" : \"Vila Paranagua\",\n               \"short_name\" : \"Vila Paranagua\",\n               \"types\" : [ \"political\", \"sublocality\", \"sublocality_level_1\" ]\n            },\n            {\n               \"long_name\" : \"São Paulo\",\n               \"short_name\" : \"São Paulo\",\n               \"types\" : [ \"administrative_area_level_2\", \"political\" ]\n            },\n            {\n               \"long_name\" : \"São Paulo\",\n               \"short_name\" : \"SP\",\n               \"types\" : [ \"administrative_area_level_1\", \"political\" ]\n            },\n            {\n               \"long_name\" : \"Brazil\",\n               \"short_name\" : \"BR\",\n               \"types\" : [ \"country\", \"political\" ]\n            },\n            {\n               \"long_name\" : \"03808-170\",\n               \"short_name\" : \"03808-170\",\n               \"types\" : [ \"postal_code\" ]\n            }\n         ],\n         \"formatted_address\" : \"Rua Victória Simionato, 93 - Vila Paranagua, São Paulo - SP, 03808-170, Brazil\",\n         \"geometry\" : {\n            \"location\" : {\n               \"lat\" : -23.4944043,\n               \"lng\" : -46.47770999999999\n            },\n            \"location_type\" : \"ROOFTOP\",\n            \"viewport\" : {\n               \"northeast\" : {\n                  \"lat\" : -23.4930553197085,\n                  \"lng\" : -46.47636101970849\n               },\n               \"southwest\" : {\n                  \"lat\" : -23.4957532802915,\n                  \"lng\" : -46.47905898029149\n               }\n            }\n         },\n         \"place_id\" : \"ChIJ11q5AgBhzpQRs1g5VtkE6so\",\n         \"plus_code\" : {\n            \"compound_code\" : \"GG4C+6W São Paulo, State of São Paulo, Brazil\",\n            \"global_code\" : \"588MGG4C+6W\"\n         },\n         \"types\" : [ \"street_address\" ]\n      }\n   ],\n   \"status\" : \"OK\"\n}";

        [Fact]
        public void ShouldGetGeocoding()
        {
            var geocodingService = new GeocodingService();
            mockGeocodingRepository.Setup(m => m.GeocodingByAddress(mockHttpClientFactory.Object, mockedGeocodingApiKey, It.IsAny<string>())).Returns(mockedJson);
            var address = geocodingService.GeocodingByAddress(mockHttpClientFactory.Object, mockGeocodingRepository.Object, mockedGeocodingApiKey, validAddress);
            mockGeocodingRepository.Verify(m => m.GeocodingByAddress(It.IsAny<IHttpClientFactory>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.True(Math.Abs(address.Latitude - (-23.4944043)) <= 0.01);
            Assert.True(Math.Abs(address.Longitude - (-46.47905898029149)) <= 0.01);
        }

        [Fact]
        public void ShouldNotGetGeocoding()
        {
            var geocodingService = new GeocodingService();
            mockGeocodingRepository.Setup(m => m.GeocodingByAddress(mockHttpClientFactory.Object, mockedGeocodingApiKey, It.IsAny<string>())).Returns("");
            var address = geocodingService.GeocodingByAddress(mockHttpClientFactory.Object, mockGeocodingRepository.Object, mockedGeocodingApiKey, validAddress);
            mockGeocodingRepository.Verify(m => m.GeocodingByAddress(It.IsAny<IHttpClientFactory>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.Equal(0, address.Latitude);
            Assert.Equal(0, address.Longitude);
        }
    }
}
