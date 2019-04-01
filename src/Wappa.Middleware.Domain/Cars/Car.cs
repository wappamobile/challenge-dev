using Wappa.Middleware.Domain.Common;
using Wappa.Middleware.Domain.Drivers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wappa.Middleware.Domain.Cars
{
    public class Car : Entity<int>
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
        [Required]
        public int DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }
    }
}
