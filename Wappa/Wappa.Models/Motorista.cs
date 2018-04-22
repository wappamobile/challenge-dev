using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.Models
{
    /// <summary>
    /// Entidade Motorista
    /// </summary>
    public class Motorista
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string UltimoNome { get; set; }

        public Endereco Endereco { get; set; }

        public Carro Carro { get; set; }
    }
}
