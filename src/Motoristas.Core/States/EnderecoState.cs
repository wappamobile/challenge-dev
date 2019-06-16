namespace Motoristas.Core.States
{
    public class EnderecoState
    {
        public string Descricao { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public CoordenadasState Coordenadas { get; set; }
    }

    public class CoordenadasState
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
