using System;
using System.ComponentModel.DataAnnotations;
using Wappa.Challenge.Shared.Commands;

namespace Wappa.Challenge.Domain.Commands.Inputs
{
    public class DeleteDriverCommand : ICommand
    {
        [Required]
        public Guid Id { get; set; }
    }
}