using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.DTO
{
    public class EnderecoDTO : BaseDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; set; }

        public string Complemento { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "O bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O estado é obrigatório.")]
        public string Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP no formato inválido.")]
        public string CEP { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "As coordenadas geográficas são obrigatórias.")]
        public CoordenadaGeograficaDTO CoordenadaGeografica { get; set; }
    }
}
