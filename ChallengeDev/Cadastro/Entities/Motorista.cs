using Cadastro.Interface;

namespace Cadastro.Entities
{
    public class Motorista : IMotorista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public virtual  Endereco Endereco { get; set; }

        public virtual  Veiculo Veiculo { get; set; }

    }
}
