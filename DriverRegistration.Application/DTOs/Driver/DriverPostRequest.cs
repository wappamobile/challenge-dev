using System.ComponentModel.DataAnnotations;

namespace DriverRegistration.Application.DTOs.Driver
{
    public class DriverPostRequest
    {
        #region Properties
        [Required(ErrorMessage = "O campo Primeiro nome é obrigatório."), MaxLength(500, ErrorMessage = "O tamanho máximo do campo Primeiro nome é de 500 caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório."), MaxLength(500, ErrorMessage = "O tamanho máximo do campo Sobrenome é de 500 caracteres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O cammpo DriverId é obrigatório. Passe um Id de motorista válido.")]
        public int DriverId { get; set; }
        #endregion
    }
}
