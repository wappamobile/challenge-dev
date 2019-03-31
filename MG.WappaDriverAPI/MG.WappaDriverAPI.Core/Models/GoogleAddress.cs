using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MG.WappaDriverAPI.Core.Models
{
    public class GoogleAddress
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public Address ToAddress()
        {
            var result = new Address();
            var googleAddress = Results.First();
            foreach (AddressComponent component in googleAddress.AddressComponents)
            {
                if (component.Types.Contains("street_number"))
                {
                    result.StreetNumber = Convert.ToInt32(component.LongName);
                }
                else if (component.Types.Contains("route"))
                {
                    result.StreetOrAddress = component.LongName;
                }
                else if (component.Types.Contains("sublocality_level_1") || component.Types.Contains("neighborhood"))
                {
                    result.Neighborhood = component.LongName;
                }
                else if (component.Types.Contains("administrative_area_level_2"))
                {
                    result.StateOrProvince = component.LongName;
                }
                else if (component.Types.Contains("administrative_area_level_1"))
                {
                    result.City = component.LongName;
                }
                else if (component.Types.Contains("country"))
                {
                    result.Country = component.LongName;
                }
                else if (component.Types.Contains("postal_code"))
                {
                    result.ZipOrPostcode = component.LongName;
                }
            }

            result.Latitude = googleAddress.Geometry.Location.Lat;
            result.Longitude = googleAddress.Geometry.Location.Lng;

            return result;
        }
    }

    public class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lng")]
        public double Lng { get; set; }
    }


    public class Viewport
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }

    public class Result
    {
        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }




}
