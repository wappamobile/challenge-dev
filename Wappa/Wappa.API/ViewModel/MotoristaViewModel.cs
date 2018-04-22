using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.API.ViewModel
{
    /// <summary>
    /// Model de Motorista
    /// </summary>
    public class MotoristaViewModel
    {
        /// <summary>
        /// Identificador do motorista
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Primeiro nome
        /// </summary>
        [Required(ErrorMessage = "Nome do motorista é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Ultimo nome
        /// </summary>
        [Required(ErrorMessage = "Último nome do motorista é obrigatório")]
        public string UltimoNome { get; set; }

        /// <summary>
        /// Endereço do motorista
        /// </summary>
        public EnderecoViewModel Endereco { get; set; }

        /// <summary>
        /// Carros do motorista
        /// </summary>
        public CarroViewModel Carro { get; set; }
    }
}
