
using System.Collections.Generic;

namespace Wappa.Driver.Api.Data.Models
{
    /// <summary>
    /// Model do carro
    /// </summary>
    public class DriverCar
    {
        /// <summary>
        /// Id do carro
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// Id do motorista
        /// </summary>
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        /// <summary>
        /// Marca do carro
        /// </summary>
        public string Make { get; set; }
        /// <summary>
        /// Modelo do caroo
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Placa do carro
        /// </summary>
        public string Plate { get; set; }
    }
}
