using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteDev.Servicos.Entidades
{
    public class Localizacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude  { get; set; }
    }
}
