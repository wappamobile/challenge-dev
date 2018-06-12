using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wappa.Challenge.ApplicationCore.Entities
{
    public partial class Endereco : ABaseEntity
    {
        [Key]
        public long IdEndereco { get; set; }
        
        [ForeignKey("Motorista")]
        [Required]
        public long IdMotorista { get; set; }

        [StringLength(200)]
        public string Logradouro { get; set; }

        [StringLength(20)]
        public string Numero { get; set; }

        [StringLength(120)]
        public string Bairro { get; set; }

        [StringLength(160)]
        public string Complemento { get; set; }

        [StringLength(100)]
        public string Cidade { get; set; }

        [StringLength(2, ErrorMessage = "Insira somente a sigla do Estado")]
        public string Estado { get; set; }

        [StringLength(9)]
        public string Cep { get; set; }

        [StringLength(20)]
        public string Latitude { get; set; }

        [StringLength(20)]
        public string Longitude { get; set; }

        public virtual Motorista Motorista { get; set; }
    }
}
