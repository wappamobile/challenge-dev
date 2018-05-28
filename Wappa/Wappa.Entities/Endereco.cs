using System.ComponentModel.DataAnnotations;

namespace Wappa.Entities
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string CEP { get; set; }        

        [Required]
		public string Cidade { get; set; }

        [Required]
		public string Estado { get; set; }

        [Required]
		public string Pais { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}