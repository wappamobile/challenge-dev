using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TesteDev.Infra.Utils;

namespace TesteDev.Infra.Entidades
{
    public abstract class EntidadeBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataCriacao { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataAlteracao { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? DataRemovido { get; set; }

        public bool Ativo { get; set; }
    }
}
