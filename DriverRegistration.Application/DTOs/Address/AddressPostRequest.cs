using System.ComponentModel.DataAnnotations;

namespace DriverRegistration.Application.DTOs.Address
{
    public class AddressPostRequest
    {
        #region Properties
        [Required(ErrorMessage = "DriverId é um campo obrigatório!")]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Logradouro é um campo obrigatório!"), MaxLength(255, ErrorMessage = "O tamanho máximo do campo Logradouro é de 255 caracteres.")]
        public string AddressName { get; set; }

        [Required(ErrorMessage = "Número é um campo obrigatório.")]
        public int Number { get; set; } 

        [MaxLength(255, ErrorMessage ="O tamanha máximo do campo Bairro é de 255 caracteres.")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "CEP é um campo obrigatório."), MaxLength(9, ErrorMessage = "O tamanho máximo do campo CEP é de 9 caracteres.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Estado é um campo obrigatório."), MaxLength(2, ErrorMessage = "O tamanho máximo do campo Estado é de 2 caracteres, utilize a sigla do estado.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Cidade é um campo obrigatório."), MaxLength(200, ErrorMessage = "O tamanho máximo do campo Cidade é de 200 caracteres.")]
        public string City { get; set; }
        #endregion
    }
}
