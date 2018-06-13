using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.Composition;
using Newtonsoft.Json;
using Wappa.Contracts;
using Wappa.Contracts.Models;

namespace Wappa.GeoLocalization
{
    public class GeoLocator : IGeoLocator
    {
        public async Task<Location> GetLocation(IConfiguration configuration, string address)
        {
            try
            {
                var url = configuration["googleApi:geocodingUrl"];
                var key = configuration["googleApi:key"];

                var uri = new Uri($"{url}?address=\"{address}\"&key={key}".Replace(@"\", "/"));

                string result = "";

                using (var client = new HttpClient())
                {
                    result = await client.GetStringAsync(uri);
                }

                var response = JsonConvert.DeserializeObject<GeoCodeResponse>(result); 

                return new Location
                {
                    Latitude = response.results[0].geometry.location.lat,
                    Longitude = response.results[0].geometry.location.lng
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
