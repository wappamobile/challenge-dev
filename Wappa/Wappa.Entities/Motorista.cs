using System.ComponentModel.DataAnnotations;

namespace Wappa.Entities
{
    public class Motorista 
    {
        public int Id { get; set; }

        [Required]
        public string PrimeiroNome { get; set; }

        [Required]
        public string UltimoNome { get; set; }

        [Required]
        public int CarroId { get; set; }
        public Carro Carro { get; set; }

        [Required]
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}