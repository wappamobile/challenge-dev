using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entity
{
    public class GeoLocation
    {
        public int GeoLocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoLocation()
        {

        }

        public GeoLocation(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

    }
}
