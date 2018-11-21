using MediatR;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class CreateDriverRequest : IRequest<Response>
    {
        [Required]
        [StringLength(11, MinimumLength = 11)]
        [Display(Name = "Document")]
        public string Document { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}