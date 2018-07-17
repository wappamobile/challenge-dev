using System.ComponentModel.DataAnnotations;

namespace DriverRegistration.Application.DTOs.Address
{
    public class AddressResponse
    {
        #region Properties
        [Display(Name = "id")]
        public int Id { get; set; }

        [Display(Name = "logradouro")]
        public string AddressName { get; set; }

        [Display(Name = "número")]
        public int Number { get; set; }

        [Display(Name = "bairro")]
        public string Neighborhood { get; set; }

        [Display(Name = "cep")]
        public string PostalCode { get; set; }

        [Display(Name = "estado")]
        public string State { get; set; }

        [Display(Name = "longitude")]
        public decimal Longitude { get; set; }

        [Display(Name = "latitude")]
        public decimal Latitude { get; set; }

        [Display(Name = "cidade")]
        public string City { get; set; }
        #endregion
    }
}
