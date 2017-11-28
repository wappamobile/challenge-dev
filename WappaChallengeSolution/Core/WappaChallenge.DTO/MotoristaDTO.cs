using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.DTO
{
    public class MotoristaDTO : BaseDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O primeiro nome é obrigatório.")]
        public string PrimeiroNome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O primeiro nome é obrigatório.")]
        public string UltimoNome { get; set; }

        [Required(ErrorMessage = "O veículo é obrigatório.")]
        public VeiculoDTO Veiculo { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public EnderecoDTO Endereco { get; set; }
    }
}
