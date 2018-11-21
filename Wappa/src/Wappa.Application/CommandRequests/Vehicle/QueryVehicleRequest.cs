using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class QueryVehicleRequest : IRequest<Response>
    {
        public QueryVehicleRequest(int driverId)
        {
            DriverId = driverId;
        }

        [Required]
        public int DriverId { get; set; }
    }
}