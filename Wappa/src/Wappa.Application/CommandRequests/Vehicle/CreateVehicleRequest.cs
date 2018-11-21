using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class CreateVehicleRequest : IRequest<Response>
    {
        [JsonIgnore]
        public int DriverId { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 7)]
        [Display(Name = "Plate")]
        [RegularExpression(@"^[a-zA-Z]{3}\d{4}$")]
        public string Plate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Fabricator")]
        public string Fabricator { get; set; }

        public CreateVehicleRequest ApplyDriverId(int driverId)
        {
            DriverId = driverId;
            return this;
        }
    }
}