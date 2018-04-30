using System.ComponentModel.DataAnnotations;

namespace Wappa.Challenge.Domain.Commands.Inputs
{
    public class AddressCommand
    {
        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        public string Complement { get; set; }

        [Required]
        public string Neighborhood { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{Street}, {Number}, {Neighborhood}, {City}, {State}, {Country}, {ZipCode}";
        }
    }
}