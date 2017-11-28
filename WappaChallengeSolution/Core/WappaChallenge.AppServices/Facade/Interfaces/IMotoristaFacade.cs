using System.Collections.Generic;
using WappaChallenge.DTO;

namespace WappaChallenge.AppServices.Facade.Interfaces
{
    public interface IMotoristaFacade
    {
        MotoristaDTO CadastrarMotorista(MotoristaDTO dto);

        ICollection<MotoristaDTO> ObterTodosOrdenadoPorPrimeiroNome();

        ICollection<MotoristaDTO> ObterTodosOrdenadoPorUltimoNome();

        MotoristaDTO AtualizarMotorista(MotoristaDTO dto);

        void ExcluirMotorista(int id);
    }
}
