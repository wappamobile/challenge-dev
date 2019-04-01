using Wappa.Middleware.Domain.Common;

namespace Wappa.Middleware.Application.Cars.Dto
{
    public class CarDto : Entity<int>
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Plate { get; set; }

        public int DriverId { get; set; }
    }
}
