using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Domain.DriverAggregation
{
    public class UpdateDriverDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Car Car { get; set; }
        public string Address { get; set; }
    }
}
