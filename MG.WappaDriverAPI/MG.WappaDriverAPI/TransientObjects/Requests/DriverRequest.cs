using System;
using System.Collections.Generic;

namespace MG.WappaDriverAPI.TransientObjects.Requests
{
    public class DriverRequest
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public CarRequest Car { get; set; }
        
    }
}
