using System;

namespace Wappa.Challenge.Domain.Commands.Outputs
{
    public class DriverResult
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CarResult Car { get; set; }
        public AddressResult Address { get; set; }
    }
}