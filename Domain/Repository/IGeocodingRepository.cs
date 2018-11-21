using System.Net.Http;

namespace Domain.Repository
{
    public interface IGeocodingRepository
    {
        string GeocodingByAddress(IHttpClientFactory clientFactory, string Key, string address);
    }
}
