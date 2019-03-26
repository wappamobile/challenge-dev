using System.Collections.Generic;
using Cadastro.Entities;

namespace Cadastro.Interface
{
    public interface ICadastroModel
    {
        Motorista RetornaPorId(int id);
        bool NovoCadastro(Motorista novoMotorista);
        bool DeleteCadastro(int id);
        bool AtualizaCadastro(Motorista motorista);

        IEnumerable<Motorista> RetornaTodos();
        IEnumerable<Motorista> RetornaPorNomeAlfabeticamente(string nome);
        IEnumerable<Motorista> RetornaPorSobreNomeAlfabeticamente(string sobrenome);
    }
}