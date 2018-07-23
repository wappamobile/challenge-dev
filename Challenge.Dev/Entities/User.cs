using System.ComponentModel.DataAnnotations;

namespace Challenge.Dev.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(25, ErrorMessage = "First name may not be longer than 25 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(25, ErrorMessage = "Last name may not be longer than 25 characters")]
        public string LastName { get; set; }

        public virtual Car Car { get; set; }

        public virtual Address Address { get; set; }
    }
}
