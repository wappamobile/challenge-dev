using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.API.ViewModel
{
    /// <summary>
    /// Model de Carro
    /// </summary>
    public class CarroViewModel
    {
        /// <summary>
        /// Identificador do carro
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Marca do carro
        /// </summary>
        [Required(ErrorMessage = "Marca é obrigatória")]
        public string Marca { get; set; }

        /// <summary>
        /// Modelo do Carro
        /// </summary>
        [Required(ErrorMessage = "Modelo é obrigatório")]
        public string Modelo { get; set; }

        /// <summary>
        /// Placa do carro
        /// </summary>
        [Required(ErrorMessage = "Placa é obrigatória")]
        public string Placa { get; set; }
    }
}
