using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MG.WappaDriverAPI.Core.Models;
using MG.WappaDriverAPI.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace MG.WappaDriverAPI.Core.Services
{
    public class GoogleApiMapsService : IGoogleApiMapsService
    {
        private readonly string _appKey;

        private readonly string _formatUrl;

        public GoogleApiMapsService(string appKey, string formatUrl)
        {
            _appKey = appKey;
            _formatUrl = formatUrl;
        }

        public GoogleAddress GetAddressFromGoogle(string address)
        {
            using (HttpClient client=new HttpClient())
            {
                string url = string.Format(_formatUrl, address, _appKey);
                //client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = client.GetStringAsync(url).Result;

                return JsonConvert.DeserializeObject<GoogleAddress>(json);
            }
        }

    }
}
