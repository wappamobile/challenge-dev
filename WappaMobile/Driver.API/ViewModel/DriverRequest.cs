using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Model;

namespace WappaMobile.Driver.API.ViewModel
{
    public class DriverRequest
    {
        public FullName Name { get; set; }

        public Vehicle Vehicle { get; set; }

        public string Address { get; set; }
    }
}
