using System;
using System.Collections.Generic;
using System.Text;

namespace DriverMgr.TO
{
    public class DriverTO
    {
        public long DriverId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? ManufacturerId { get; set; }

        public string Model { get; set; }

        public string Plate { get; set; }

        public string Address { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
    }
}
