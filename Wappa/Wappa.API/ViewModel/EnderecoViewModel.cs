using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.API.ViewModel
{
    /// <summary>
    /// Model de Endereço
    /// </summary>
    public class EnderecoViewModel
    {
        /// <summary>
        /// Identificador do endereço
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da Rua 
        /// </summary>
        [Required(ErrorMessage = "Rua é obrigatória")]
        public string Rua { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        [Required(ErrorMessage = "Número é obrigatório")]
        public int Numero { get; set; }

        /// <summary>
        /// Complemento(Apto, Casa, etc...)
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        [Required(ErrorMessage = "CEP é obrigatório")]
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string Cidade { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [MaxLength(2, ErrorMessage = "Estado deve ter no máximo 2 caracteres")]
        public string UF { get; set; }

        /// <summary>
        /// Latitude(Obtido através do google maps)
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Longitude(obtigado através do google maps
        /// </summary>
        public string Longitude { get; set; }
    }
}
