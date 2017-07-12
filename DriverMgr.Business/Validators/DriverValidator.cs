using DriverMgr.TO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMgr.Business.Validators
{
    public class DriverValidator : ValidatorBase
    {
        public long DriverId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(4)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "First Name")]
        [MinLength(4)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public int? ManufacturerId { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{3}[-]\d{4}", ErrorMessage = "The {0} need to be in the format: 'AAA-9999'.")]
        public string Plate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double? Longitude { get; set; }

        [Required]
        public double? Latitude { get; set; }

        public static DriverValidator From(DriverTO to)
        {
            return new DriverValidator
            {
                DriverId = to.DriverId,
                FirstName = to.FirstName,
                LastName = to.LastName,
                ManufacturerId = to.ManufacturerId,
                Model = to.Model,
                Plate = to.Plate,
                Address = to.Address,
                Longitude = to.Longitude,
                Latitude = to.Latitude
            };
        }
    }
}