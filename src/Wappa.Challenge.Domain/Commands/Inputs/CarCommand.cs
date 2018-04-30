using System.ComponentModel.DataAnnotations;

namespace Wappa.Challenge.Domain.Commands.Inputs
{
    public class CarCommand
    {
        [Required]
        public string Model { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Manufacturer { get; set; }
    }
}