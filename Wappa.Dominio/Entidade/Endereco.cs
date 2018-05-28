namespace Wappa.Dominio.Entidade
{
    public class Endereco
    {
        public int Id { get; set; }
        public int IdMotorista { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public Motorista Motorista { get; set; }
    }
}