
using System.Collections.Generic;

namespace Wappa.Driver.Api.Data.Models
{
    /// <summary>
    /// Model do motorista
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Id do motorista
        /// </summary>
        public int DriverID { get; set; }
        /// <summary>
        /// Nome do motorista
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Sobrenome do motorista
        /// </summary>
        public string LastName { get; set; }
    }
}
