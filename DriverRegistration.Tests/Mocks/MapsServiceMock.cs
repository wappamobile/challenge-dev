using DriverRegistration.Domain.Services.Interfaces;
using DriverRegistration.Domain.Services.ValueObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DriverRegistration.Tests.Mocks
{
    public class MapsServiceMock : IMapsService
    {
        public async Task<GeocodeResponseVO> GetCoordinates(string address)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GeocodeResponseVO>("{  \"results\": [    {      \"addressComponents\": [        {          \"longName\": \"976\",          \"shortName\": \"976\",          \"types\": [            \"street_number\"          ]        },        {          \"longName\": \"Rua Pamplona\",          \"shortName\": \"R. Pamplona\",          \"types\": [            \"route\"          ]        },        {          \"longName\": \"Jd Paulista\",          \"shortName\": \"Jd Paulista\",          \"types\": [            \"political\",            \"sublocality\",            \"sublocality_level_1\"          ]        },        {          \"longName\": \"São Paulo\",          \"shortName\": \"São Paulo\",          \"types\": [            \"administrative_area_level_2\",            \"political\"          ]        },        {          \"longName\": \"São Paulo\",          \"shortName\": \"SP\",          \"types\": [            \"administrative_area_level_1\",            \"political\"          ]        },        {          \"longName\": \"Brazil\",          \"shortName\": \"BR\",          \"types\": [            \"country\",            \"political\"          ]        },        {          \"longName\": \"01405-200\",          \"shortName\": \"01405-200\",          \"types\": [            \"postal_code\"          ]        }      ],      \"formattedAddress\": \"R. Pamplona, 976 - Jd Paulista, São Paulo - SP, 01405-200, Brazil\",      \"geometry\": {        \"location\": {          \"lat\": -23.5658932,          \"lng\": -46.6558647        },        \"locationType\": \"ROOFTOP\",        \"viewport\": {          \"northeast\": {            \"lat\": -23.5645447,            \"lng\": -46.65452          },          \"southwest\": {            \"lat\": -23.5672417,            \"lng\": -46.6572151          }        }      },      \"placeId\": \"ChIJLcIECcZZzpQRItws8Ie3LeE\",      \"types\": [        \"establishment\",        \"premise\"      ]    }  ],  \"status\": \"OK\"}");
        }
    }
}
