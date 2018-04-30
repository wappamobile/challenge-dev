using System.Linq;
using System.Net;
using RestSharp;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Domain.Commands.Outputs;
using Wappa.Challenge.Domain.Services;
using Wappa.Challenge.Shared.Configuration;

namespace Wappa.Challenge.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly GoogleMapsConfiguration _configuration;

        public GoogleService(GoogleMapsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (double? latitude, double? longitude) GetCoordinates(AddressCommand address)
        {
            var addressFormated = WebUtility.UrlEncode(address.ToString());

            var url = $"{_configuration.Url}?address={addressFormated}&key={_configuration.Key}";
            var request = new RestRequest(Method.GET);
            var client = new RestClient(url);
            var result = client.ExecuteAsGet<CoordinatesResult>(request, "GET");

            var location = result?.Data?.Results?.FirstOrDefault()?.Geometry?.Location;

            if (location == null || !result.IsSuccessful)
                return (null, null);

            return (location.Lat, location.Lng);
        }
    }
}