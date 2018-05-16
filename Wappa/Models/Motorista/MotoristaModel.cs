using System.ComponentModel.DataAnnotations;

namespace Wappa.Models.Motorista {
    public class MotoristaModel {
        [Required]
        public string PrimeiroNome { get; set; }

        [Required]
        public string UltimoNome { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public string Rua { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public long Cep { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public string Pais { get; set; }

        public GeolocalizacaoResult Geolocalizacao { get; set; }

        public override string ToString () {
            return $"{Rua}+{Numero}+{Bairro}+{Cidade}+{Estado}+{Pais}+{Cep}";
        }
    }
}