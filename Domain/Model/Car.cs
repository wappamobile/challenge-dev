using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [StringLength(8)]
        public string Plate { get; set; }
    }
}
