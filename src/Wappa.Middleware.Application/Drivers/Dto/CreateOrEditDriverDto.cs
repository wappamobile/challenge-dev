using Wappa.Middleware.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Wappa.Middleware.Application.Drivers.Dto
{
    public class CreateOrEditDriverDto : Entity<int>
    {
        [Required]
        [StringLength(50)]
        public string FirtName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string District { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }
        [StringLength(150)]
        public string Complement { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(2)]
        public string State { get; set; }
    }
}
