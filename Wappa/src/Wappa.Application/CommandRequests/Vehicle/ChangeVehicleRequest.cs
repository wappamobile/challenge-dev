using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class ChangeVehicleRequest : IRequest<Response>
    {
        [JsonIgnore]
        public int Id { get; set; }

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

        public ChangeVehicleRequest ApplyIds(int driverId, int id)
        {
            DriverId = driverId;
            Id = id;
            return this;
        }
    }
}