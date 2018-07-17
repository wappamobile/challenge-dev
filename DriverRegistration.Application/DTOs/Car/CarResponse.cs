using System.ComponentModel.DataAnnotations;

namespace DriverRegistration.Application.DTOs.Car
{
    public class CarResponse
    {
        #region Properties
        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "brand")]
        public string Brand { get; set; }

        [Display(Name = "model")]
        public string Model { get; set; }

        [Display(Name = "plate")]
        public string Plate { get; set; }
        #endregion
    }
}
