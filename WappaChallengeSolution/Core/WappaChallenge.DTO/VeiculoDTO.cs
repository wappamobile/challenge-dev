using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.DTO
{
    public class VeiculoDTO : BaseDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A marca é obrigatória.")]
        public string Marca { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O modelo é obrigatória.")]
        public string Modelo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A placa é obrigatória.")]
        [RegularExpression(@"^[A-Z]{3}-\d{4}$", ErrorMessage = "Placa no formato inválido.")]
        public string Placa { get; set; }
    }
}
