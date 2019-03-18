using System;

namespace DriverCatalogService.Models
{
    public class Driver
    {
        public string Id { get; set; }

        public Name Name { get; set; }
        public Car Car { get; set; }
        public Address Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}