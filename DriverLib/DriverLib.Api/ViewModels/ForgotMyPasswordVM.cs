using System.ComponentModel.DataAnnotations;

namespace DriverLib.Api.ViewModels
{
    public class ForgotMyPasswordVM
    {
        [Required]
        public string Email { get; set; }
    }
}
