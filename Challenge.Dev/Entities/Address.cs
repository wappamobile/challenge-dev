using Challenge.Dev.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Challenge.Dev.Models
{
    public class Address
    {
        [Key]
        public long Id { get; set; }
        public string address { get; set; }
        public int number { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string postalcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        [ForeignKey("User")]
        public long IdUser { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public void GetGoogleGeoCode()
        {
            var _geoCodeResponse = Helper.GetGeocode<GoogleGeoCodeResponse>(
                  this.address,
                  this.number.ToString(),
                  this.neighborhood,
                  this.city,
                  this.state,
                  this.country);

            if (_geoCodeResponse?.results?.Count() > 0)
            {
                var _location = _geoCodeResponse.results.FirstOrDefault();
                this.latitude =  _location.geometry.location.lat;
                this.longitude = _location.geometry.location.lng;
            }
        }
    }
}
