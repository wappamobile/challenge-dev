using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MG.WappaDriverAPI.Core.Models;
using MG.WappaDriverAPI.Core.Services;
using MG.WappaDriverAPI.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace MG.WappaDriverAPI.Test.Mock
{
    public class GoogleApiMapsServiceMock : IGoogleApiMapsService
    {
        private readonly Dictionary<string,string> _jsons=new Dictionary<string, string>()
        {
            {"R. Ramos Batista, 198 - Vila Olímpia","json//wappabr.json"},
            {"600 E Washington Blvd","json//wappausa.json"},
            {"Rua Henrique Schaumann, 600","json//Schaumann.json"},
            {"Rua Vergueiro, 2016 - Vila Mariana","json//vergueiro.json"},
        };


        public GoogleAddress GetAddressFromGoogle(string address)
        {
            try
            {
                string file = _jsons.FirstOrDefault(a => address.Contains(a.Key)).Value;
                file = $"{Directory.GetCurrentDirectory()}\\{file}";
                string json = File.ReadAllText(file);
                return JsonConvert.DeserializeObject<GoogleAddress>(json);
            }
            catch (Exception e)
            {
                return new GoogleAddress();
            }
        }
        
    }
}
