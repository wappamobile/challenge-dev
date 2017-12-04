using System;
using System.Globalization;

namespace Wappa.Service.GeometryService
{
    public class Geometry
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public bool IsValid { get; private set; }

        public Geometry(bool isValid)
        {
            this.IsValid = isValid;
        }


        public Geometry(string latitude, string longitude) : this(true)
        {
            this.Latitude = Convert.ToDouble(latitude, CultureInfo.GetCultureInfo("en-US"));
            this.Longitude = Convert.ToDouble(longitude, CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
