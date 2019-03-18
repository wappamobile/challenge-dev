using System;
using DriverCatalogService.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DriverCatalogService.Infrastructure
{
    public class GoogleGeoLocator : IGeoLocator
    {
        private string _apiKey;

        public GoogleGeoLocator(IConfiguration configuration)
        {
            _apiKey = configuration["Google:APIKey"];
        }

        public Address LocateAddress(string addressFullAddress)
        {
            var client = new RestClient("https://maps.googleapis.com/");
            var request = new RestRequest("maps/api/geocode/json");
            request.AddParameter("address", addressFullAddress);
            request.AddParameter("key", _apiKey);

            var response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new ApplicationException(response.ErrorMessage);
            }

            var doc = JObject.Parse(response.Content);
            if (doc.ContainsKey("error_message"))
            {
                throw new ApplicationException(doc["error_message"].ToString());
            }

            return new Address
            {
                FullAddress = doc.SelectToken("results[0].formatted_address").Value<string>(),
                Longitude = doc.SelectToken("results[0].geometry.location.lng").Value<string>(),
                Latitude = doc.SelectToken("results[0].geometry.location.lat").Value<string>()
            };
        }
    }
}