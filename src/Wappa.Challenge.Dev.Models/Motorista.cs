namespace Wappa.Challenge.Dev.Models
{
    public class Motorista : Entidade
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string NomeCompleto => $"{Nome} {SobreNome}";
        public Carro Carro { get; set; }
        public Endereco Endereco { get; set; }
    }
}
