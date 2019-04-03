using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wappa.Application.ViewModels
{
    public class DriverViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The LastName is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Last Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Car Model")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "The Car Brand is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Car Brand")]
        public string CarBrand { get; set; }

        [Required(ErrorMessage = "The Car Plate is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Car Plate")]
        public string CarPlate { get; set; }

        [Required(ErrorMessage = "The Zipcode is Required")]
        [MinLength(9)]
        [MaxLength(9)]
        [DisplayName("Zipcode")]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "The Adress is Required")]
        [MinLength(10)]
        [MaxLength(500)]
        [DisplayName("Address")]
        public string Address { get; set; }
                
        public string Coordinates { get; set; }


    }
}
