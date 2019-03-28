using DriverRegistration.Domain.Services.Interfaces;
using DriverRegistration.Domain.Services.ValueObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DriverRegistration.Domain.Services
{
    public class MapsService : IMapsService
    {
        public async Task<GeocodeResponseVO> GetCoordinates(string address)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44391/api/Maps/?address={address}");
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<GeocodeResponseVO>(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
