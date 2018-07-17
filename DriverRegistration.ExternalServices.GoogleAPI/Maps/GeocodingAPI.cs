using DriverRegistration.Domain.Interfaces;
using DriverRegistration.ExternalServices.GoogleAPI.Maps.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DriverRegistration.ExternalServices.GoogleAPI.Maps
{
    public class GeocodingAPI: IExternalApiGeocoding
    {
        #region Constructors
        public GeocodingAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Attributes
        private readonly IConfiguration _configuration;
        #endregion

        #region Properties

        #endregion

        #region Methods
        public IDictionary<String, Decimal> GetGeoCordinates(String address)
        {
            Dictionary<String, Decimal> response = new Dictionary<string, decimal>();
            String _reponseService = String.Empty;
            string key = _configuration["AppConfiguration:Maps"].ToString();

            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                _reponseService = wc.DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=" + key);
            }

            GeoCodeResponse _geo = null;

            if (_reponseService.Length > 2)
            {
                _geo = JsonConvert.DeserializeObject<GeoCodeResponse>(_reponseService);

                if (_geo != null && _geo.results != null && _geo.results.Count > 0)
                {
                    if (!response.ContainsKey("longitude"))
                        response.Add("longitude", Convert.ToDecimal(_geo.results[0].geometry.location.lng));

                    if (!response.ContainsKey("latitude"))
                        response.Add("latitude", Convert.ToDecimal(_geo.results[0].geometry.location.lat));
                }
            }


            return response;
        }
        #endregion
    }
}
