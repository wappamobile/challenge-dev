using DriverLib.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverLib.Domain
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
    }
}
