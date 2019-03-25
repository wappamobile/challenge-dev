using Cadastro.Entities;

namespace Cadastro.Interface
{
    public interface ICadastroModel
    {
        bool NovoCadastro(Motorista novoMotorista);
        bool DeleteCadastro(int id);
        bool AtualizaCadastro(Motorista motorista);
    }
}