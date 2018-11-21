using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class GeocodingRepository : IGeocodingRepository
    {
        public string GeocodingByAddress(IHttpClientFactory clientFactory, string key, string address)
        {
            var builder = new StringBuilder();
            builder.Append("https://maps.googleapis.com/maps/api/geocode/json?address=");
            builder.Append($"{address}&");
            builder.Append($"key={key}");
            var task = Communicate(clientFactory, key, builder.ToString());
            task.Wait();
            return task.Result;
        }

        static async Task<string> Communicate(IHttpClientFactory clientFactory, string key, string url)
        {
            var response = clientFactory.CreateClient().GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                return responseContent.ReadAsStringAsync().Result;
            }
            throw new HttpRequestException("Problem with the Geocoding API communication");
        }
    }
}
