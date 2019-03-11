namespace Driver.Application.Data.Entities
{
    /// <summary>
    /// Estrutura de endereço do motorista
    /// </summary>
    public class AddressEntity
    {
        /// <summary>
        /// Número do endereço
        /// </summary>
        public string AddressNumber { get; set; }

        /// <summary>
        /// Descrição do endereço
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Bairro do endereço
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Cidade do endereço
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Estado do endereço
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Cep do endereço
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Latitude do endereço
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude do endereço
        /// </summary>
        public double? Longitude { get; set; }

    }
}