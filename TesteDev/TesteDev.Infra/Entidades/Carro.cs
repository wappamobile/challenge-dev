using System.ComponentModel.DataAnnotations;

namespace TesteDev.Infra.Entidades
{
    public class Carro : EntidadeBase
    {
        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public string Placa { get; set; }
    }
}
