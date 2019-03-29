using Challenge.Domain.DriverAggregation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Challenge.Infra.Google
{
    public class GoogleGeocodingService : IGeocodingService
    {
        private readonly string apiKey;

        public GoogleGeocodingService(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            apiKey = configuration["GoogleApiKey"];
        }
        public string GetGeocodingByAddress(string address)
        {
            HttpClient client = new HttpClient();
            var request = client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={apiKey}");
            request.Wait();
            var data = request.Result.Content.ReadAsStringAsync();
            data.Wait();
            return data.Result;
        }
    }
}
