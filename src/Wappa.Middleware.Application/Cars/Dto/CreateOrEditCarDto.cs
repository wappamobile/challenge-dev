using Wappa.Middleware.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Wappa.Middleware.Application.Cars.Dto
{
    public class CreateOrEditCarDto : Entity<int>
    {
        [Required]
        [StringLength(50)]
        public string Brand { get; set; }
        [Required]
        [StringLength(50)]
        public string Model { get; set; }
        [Required]
        [StringLength(8)]
        public string Plate { get; set; }

        public int DriverId { get; set; }
    }
}
