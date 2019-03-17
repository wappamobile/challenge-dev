namespace WappaMobile.ChallengeDev.Models
{
    public class Motorista : Entidade
    {
        public Nome Nome { get; set; }
        public Carro Carro { get; set; }
        public Endereco Endereco { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
