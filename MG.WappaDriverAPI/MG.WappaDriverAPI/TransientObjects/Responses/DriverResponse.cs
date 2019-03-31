using System;
using System.Collections.Generic;

namespace MG.WappaDriverAPI.TransientObjects.Responses
{
    public class DriverResponse
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public CarResponse Car { get; set; }
        
        public IEnumerable<AddressResponse> Addresses { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? ModifiedAt { get; set; }
    }
}
