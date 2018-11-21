using Microsoft.Extensions.Configuration;
using Wappa.Domain.Interfaces.Connectors;

namespace Wappa.Infra.CrossCutting
{
    public class GoogleMapsConfiguration : IGoogleMapsConfiguration
    {
        private readonly IConfiguration _configuration;

        public GoogleMapsConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Key { get => _configuration["GoogleApi:Maps:Key"]; }

        public string Url { get => _configuration["GoogleApi:Maps:Url"]; }
    }
}