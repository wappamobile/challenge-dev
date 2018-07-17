using System.ComponentModel.DataAnnotations;

namespace DriverRegistration.Application.DTOs.Car
{
    public class CarPutRequest
    {
        #region Properties
        [Required(ErrorMessage ="O campo id é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo marca é obrigatório."), MaxLength(200, ErrorMessage = "O tamanho máximo do campo Marca é de 200 caracteres.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "O campo Modelo é obrigatório."), MaxLength(300, ErrorMessage = "O tamanho máximo do campo Modelo é de 300 caracteres.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "O campo Placa é obrigadório"), MaxLength(8, ErrorMessage = "O tamanho máximo do campo Placa é de 8 caracteres.")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "O cammpo DriverId é obrigatório. Passe um Id de motorista válido.")]
        public int DriverId { get; set; }
        #endregion
    }
}
