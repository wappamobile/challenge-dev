using System;
using System.ComponentModel.DataAnnotations;
using Wappa.Challenge.Shared.Commands;

namespace Wappa.Challenge.Domain.Commands.Inputs
{
    public class UpdateDriverCommand : ICommand
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public CarCommand Car { get; set; }

        [Required]
        public AddressCommand Address { get; set; }
    }
}