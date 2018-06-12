using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wappa.Challenge.ApplicationCore.Entities
{
    public partial class Carro : ABaseEntity
    {
        [Key]
        public long IdCarro { get; set; }

        [ForeignKey("Motorista")]
        [Required]
        public long IdMotorista { get; set; }

        [StringLength(60)]
        public string Marca { get; set; }

        [StringLength(80)]
        public string Modelo { get; set; }

        [StringLength(8)]
        public string Placa { get; set; }

        public virtual Motorista Motorista { get; set; }
    }
}
