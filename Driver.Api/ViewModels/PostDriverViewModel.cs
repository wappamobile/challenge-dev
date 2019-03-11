using System.ComponentModel.DataAnnotations;

namespace Driver.Api.ViewModels
{
    /// <summary>
    /// Estrutura para incluir um novo motorista
    /// </summary>
    public class PostDriverViewModel
    {
        /// <summary>
        /// Primeiro nome do motorista
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Último nome do motorista
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Informação do endereço
        /// </summary>
        [Required]
        public PostAddressViewModel Address { get; set; }

        /// <summary>
        /// Informações do carro
        /// </summary>
        [Required]
        public CarViewModel Car { get; set; }
    }
}