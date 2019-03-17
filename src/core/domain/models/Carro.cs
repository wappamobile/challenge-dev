namespace WappaMobile.ChallengeDev.Models
{
    public class Carro : Entidade
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public Placa Placa { get; set; }

        public override string ToString()
        {
            return $"{Marca} {Modelo} - {Placa}";
        }
    }
}
