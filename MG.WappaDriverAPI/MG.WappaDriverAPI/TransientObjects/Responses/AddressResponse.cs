using System;

namespace MG.WappaDriverAPI.TransientObjects.Responses
{
    public class AddressResponse
    {
        public string ZipOrPostcode  { get; set; }
        
        public string StreetOrAddress { get; set; }
        
        public string SuiteOrApartment { get; set; }
        
        public string City { get; set; }

        public int StreetNumber { get; set; }

        public string StateOrProvince { get; set; }
        
        public string Country { get; set; }

        public string Neighborhood { get; set; }

        public string Name { get; set; }
        
        public string Id { get; set; }
        
        public string DriverId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
