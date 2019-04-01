using Wappa.Middleware.Domain.Common;

namespace Wappa.Middleware.Application.Drivers.Dto
{
    public class DriverDto : Entity<int>
    {
        public string FirtName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string ZipCode { get; set; }

        public string Complement { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
