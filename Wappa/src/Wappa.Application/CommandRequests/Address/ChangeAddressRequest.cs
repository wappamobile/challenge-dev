using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Wappa.Domain.Messages;

namespace Wappa.Application.CommandRequests
{
    public class ChangeAddressRequest : IRequest<Response>
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int DriverId { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [StringLength(15, MinimumLength = 1)]
        [Display(Name = "Number")]
        public string Number { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Neighborhood")]
        public string Neighborhood { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(2, MinimumLength = 2)]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public ChangeAddressRequest ApplyIds(int driverId, int id)
        {
            DriverId = driverId;
            Id = id;
            return this;
        }
    }
}