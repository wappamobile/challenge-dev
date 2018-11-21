using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class QueryAddressRequest : IRequest<Response>
    {
        public QueryAddressRequest(int driverId)
        {
            DriverId = driverId;
        }

        [Required]
        public int DriverId { get; set; }
    }
}