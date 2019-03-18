
namespace Wappa.Driver.Api.Dtos
{
    public class DriverDto
    {
        /// <summary>
        /// Id do motorista
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// Nome do motorista
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Sobrenome do motorista
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Dados do carro do motorista
        /// </summary>
        public DriverCarDto DriverCarDto { get; set; }
        /// <summary>
        /// Dados do endereço do motorista
        /// </summary>
        public DriverAddressDto DriverAddressDto { get; set; }
    }
}
