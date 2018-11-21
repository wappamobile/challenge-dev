using System.Net.Http;
using Domain.Model;
using Domain.Repository;

namespace Domain.Service
{
    public interface IGeocodingService
    {
        Address GeocodingByAddress(IHttpClientFactory clientFactory, IGeocodingRepository geocodingRepository, string key, Address address);
    }
}
