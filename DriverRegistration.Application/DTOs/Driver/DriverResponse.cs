using DriverRegistration.Application.DTOs.Address;
using DriverRegistration.Application.DTOs.Car;
using System.ComponentModel.DataAnnotations;

namespace DriverRegistration.Application.DTOs.Driver
{
    public class DriverResponse
    {
        #region Properties
        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "primeiro_nome")]
        public string FirstName { get; set; }

        [Display(Name = "sobrenome")]
        public string LastName { get; set; }

        [Display(Name = "carro")]
        public CarResponse DriverCar { get; set; }

        [Display(Name = "edereco")]
        public AddressResponse Address { get; set; }
        #endregion
    }
}
