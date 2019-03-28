using GoogleMaps.WebApi.Models;
using GoogleMaps.WebApi.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleMaps.WebApi.Services
{
    public class MapsService : IMapsService
    {
        const string GOOGLE_MAPS_API_URL = "https://maps.googleapis.com/maps/api/geocode/json?";
        const string GOOGLE_MAPS_API_KEY = "AIzaSyAAGre2dBlJLh9O5rQdtXnhc1YGy8JqQCk";

        public async Task<GeocodeResponse> GetGeocode(string address)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{GOOGLE_MAPS_API_URL}address={address}&key={GOOGLE_MAPS_API_KEY}");
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();

                    DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
                    var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = contractResolver };

                    return JsonConvert.DeserializeObject<GeocodeResponse>(result, jsonSerializerSettings);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
