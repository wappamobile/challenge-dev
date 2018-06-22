using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Vitor.Application.Options
{
    public class GoogleMapsOptions
    {
        public const string SectionName = "GoogleMapsOptions";

        public string Url { get; private set; }
        public string Key { get; private set; }

        public GoogleMapsOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection(SectionName);
            var key = section["GoogleMapsKey"];
            var url = section["GoogleMapsUrl"];

            validateConfiguration(url, key);

            this.Url = url;
            this.Key = key;
        }

        private static void validateConfiguration(string url, string key)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new KeyNotFoundException($"Config key {SectionName}:Endpoint not set.");
            if (string.IsNullOrWhiteSpace(key))
                throw new KeyNotFoundException($"Config key {SectionName}:SavePassword not set.");
        }
    }
}
