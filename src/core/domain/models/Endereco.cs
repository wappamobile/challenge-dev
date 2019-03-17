namespace WappaMobile.ChallengeDev.Models
{
    public class Endereco
    {
        public string Tipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public Coordenadas Coordenadas { get; private set; }

        public override string ToString()
        {
            return $"{Tipo} {Logradouro},{Numero} - {Bairro}, {Cidade}, {Uf} {Cep}";
        }

        public void DefinirCoordenadas(Coordenadas coordenadas)
        {
            Coordenadas = coordenadas;
        }
    }
}
