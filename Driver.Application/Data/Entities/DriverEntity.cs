using System;

namespace Driver.Application.Data.Entities
{
    /// <summary>
    /// Dados do motorista
    /// </summary>
    public class DriverEntity : AddressEntity
    {
        /// <summary>
        /// Marca do carro
        /// </summary>
        public string CarBrand { get; set; }

        /// <summary>
        /// Placa do carro
        /// </summary>
        public string CarLicensePlate { get; set; }

        /// <summary>
        /// Modelo do carro
        /// </summary>
        public string CarModel { get; set; }

        /// <summary>
        /// Data que o motorista foi incluido
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Id do motorista
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// Primeiro nome do motorista
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Último nome do motorista
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Data que o motorista foi alterado pela última vez
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}