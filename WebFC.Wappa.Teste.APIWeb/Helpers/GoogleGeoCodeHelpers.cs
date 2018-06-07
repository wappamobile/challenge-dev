using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFC.Wappa.Teste.APIWeb.Models;

namespace WebFC.Wappa.Teste.APIWeb.Helpers
{
    public class GoogleGeoCodeHelpers
    {

        public static GoogleGeoCodeResponse GetCoordenadas(string Endereco)
        {

            string base_google = "https://maps.googleapis.com/maps/api/";
            string apiKey = "AIzaSyBJyPV5vYF9YpR-PJLoK9vYnEeGn2UcXco";

            string address = base_google + "geocode/json?address=" + Endereco + "&key=" + apiKey;
            var result = new System.Net.WebClient().DownloadString(address);
            GoogleGeoCodeResponse geoCodeResponse = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(result);

            return geoCodeResponse; 
        }
    }
}