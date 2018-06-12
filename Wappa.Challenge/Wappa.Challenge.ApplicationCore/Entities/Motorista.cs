using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wappa.Challenge.ApplicationCore.Entities
{
    [ComplexType()]
    [Serializable]
    public partial class Motorista : ABaseEntity
    {
        public Motorista()
        {
            this.Carro = new HashSet<Carro>();
            this.Endereco = new HashSet<Endereco>();
        }

        [Key]
        public long IdMotorista { get; set; }

        [StringLength(80)]
        public string Nome { get; set; }

        [StringLength(100)]
        public string Sobrenome { get; set; }

        public DateTime? DataInclusao { get; set; }

        public virtual ICollection<Carro> Carro { get; set; }

        public virtual ICollection<Endereco> Endereco { get; set; }
    }
}
