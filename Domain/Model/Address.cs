using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Country { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string Complement { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Neighborhood { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(9)]
        public string ZipCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
