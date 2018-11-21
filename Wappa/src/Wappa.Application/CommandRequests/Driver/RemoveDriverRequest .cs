using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class RemoveDriverRequest : IRequest<Response>
    {
        public RemoveDriverRequest(int id)
        {
            Id = id;
        }

        [Required]
        public int Id { get; set; }
    }
}