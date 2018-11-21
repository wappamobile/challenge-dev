using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Model
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [JsonIgnore]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [JsonIgnore]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
