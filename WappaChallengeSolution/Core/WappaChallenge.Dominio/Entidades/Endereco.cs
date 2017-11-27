using System.ComponentModel.DataAnnotations;

namespace WappaChallenge.Dominio.Entidades
{
    public class Endereco : BaseDominio<int>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "O logradouro é obrigatório.")]
        public string Logradouro { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; protected set; }

        public string Complemento { get; protected set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "O bairro é obrigatório.")]
        public string Bairro { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O estado é obrigatório.")]
        public string Estado { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP no formato inválido.")]
        public string CEP { get; protected set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "As coordenadas geográficas são obrigatórias.")]
        public CoordenadaGeografica CoordenadaGeografica { get; protected set; }

        public Endereco(
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cidade,
            string estado,
            string cep,
            CoordenadaGeografica coordenadas)
        {
            this.Logradouro = logradouro;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Estado = estado;
            this.CEP = cep;
            this.CoordenadaGeografica = coordenadas;

            this.ValidarEntidade();
        }
    }
}
