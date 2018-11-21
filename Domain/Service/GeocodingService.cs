using System.Net.Http;
using System.Text;
using Domain.Model;
using Domain.Repository;
using Newtonsoft.Json;

namespace Domain.Service
{
    public class GeocodingService: IGeocodingService
    {
        public Address GeocodingByAddress(IHttpClientFactory clientFactory, IGeocodingRepository geocodingRepository, string key, Address address)
        {
            var builder = new StringBuilder();
            builder.Append($"{address.Street.Replace(" ", "+")},+");
            builder.Append($"{address.Number},+");
            builder.Append($"{address.Neighborhood.Replace(" ", "+")},+");
            builder.Append($"{address.City.Replace(" ", "+")},+");
            builder.Append($"{address.State.Replace(" ", "+")},+");
            builder.Append($"{address.ZipCode}");

            var json = geocodingRepository.GeocodingByAddress(clientFactory, key, builder.ToString());
            var mappedResult = JsonConvert.DeserializeObject<GoogleGeocodingApiResponse>(json);

            if (mappedResult != null && mappedResult.results != null && mappedResult.results.Count > 0)
            {
                address.Latitude = mappedResult.results[0].geometry.location.lat;
                address.Longitude = mappedResult.results[0].geometry.location.lng;
            }

            return address;
        }
    }
}
