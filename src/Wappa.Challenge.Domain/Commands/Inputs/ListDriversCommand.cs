using System.ComponentModel.DataAnnotations;
using Wappa.Challenge.Shared.Commands;

namespace Wappa.Challenge.Domain.Commands.Inputs
{
    public class ListDriversCommand : ICommand
    {
        [Required]
        public OrderByOptionCommand OrderBy { get; set; }
    }
}