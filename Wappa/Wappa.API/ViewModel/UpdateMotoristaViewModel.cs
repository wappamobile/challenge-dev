using System.ComponentModel.DataAnnotations;

namespace Wappa.API.ViewModel
{
    public class UpdateMotoristaViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string PrimeiroNome { get; set; }

        [Required]
        public string UltimoNome { get; set; }

        [Required]
        public int CarroId { get; set; }

        [Required]
        public int EnderecoId { get; set; }
    }
}
