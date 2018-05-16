namespace Wappa.Models.Motorista {
    public class MotoristaResult {
        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Placa { get; set; }

        public string Rua { get; set; }
        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public long Cep { get; set; }
        public string Pais { get; set; }

        public GeolocalizacaoResult Geolocalizacao { get; set; }
    }
}