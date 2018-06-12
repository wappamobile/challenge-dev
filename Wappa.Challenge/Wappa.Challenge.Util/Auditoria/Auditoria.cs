using System;
using System.ComponentModel.DataAnnotations;

namespace Wappa.Challenge.Util.Auditoria
{
    internal class Auditoria
    {
        [Key]
        public long IdAuditoria { get; set; }
        public DateTime DataHora { get; set; }
        [Required]
        [StringLength(50)]
        public string Tabela { get; set; }
        [StringLength(500)]
        public string ChavesPrimarias { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
    }
}
