using System;
using System.Collections.Generic;
using System.Text;

namespace Vitor.Domain.Model
{
    public class Address
    {
        public string Street { get; set; }
        public long Number { get; set; }
        public Location Location { get; set; }
    }
}
