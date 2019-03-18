
namespace Wappa.Driver.Api.Dtos
{
    /// <summary>
    /// Dto do endereco do motorista
    /// </summary>
    public class DriverAddressDto
    {
        /// <summary>
        /// Id do endereco
        /// </summary>
        public int AddressId { get; set; }
        /// <summary>
        /// Id do motorista
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// Endereco
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Numero
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Bairro
        /// </summary>
        public string Neighborhood { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Pais
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public string Longitude { get; set; }
    }
}
