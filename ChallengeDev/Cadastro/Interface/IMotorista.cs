using Cadastro.Entities;

namespace Cadastro.Interface
{
    public interface IMotorista
    {   
        string Nome { get; set; }
        string Sobrenome { get; set; }
        Endereco Endereco { get; set; }
        Veiculo Veiculo { get; set; }
    }
}