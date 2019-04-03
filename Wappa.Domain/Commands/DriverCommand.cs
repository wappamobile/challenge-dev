using System;
using Wappa.Domain.Core.Commands;

namespace Wappa.Domain.Commands
{
    public abstract class DriverCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string CarModel { get; set; }

        public string CarBrand { get; set; }

        public string CarPlate { get; set; }

        public string Zipcode { get; set; }

        public string Address { get; set; }

        public string Coordinates { get; set; }
    }
}