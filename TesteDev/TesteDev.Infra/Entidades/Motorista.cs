using System.ComponentModel.DataAnnotations;
using TesteDev.Servicos.Entidades;

namespace TesteDev.Infra.Entidades
{
    public class Motorista : EntidadeBase
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string UltimoNome { get; set; }

        [Required]
        public EnderecoCompleto EnderecoCompleto { get; set; }

        [Required]
        public Carro Carro { get; set; }

        public Localizacao Localizacao { get; set; }

    }
}
