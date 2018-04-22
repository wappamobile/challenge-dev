using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;

namespace Wappa.DataAccess.Interfaces
{
    public interface IMotoristaRepository
    {
        /// <summary>
        /// Retorna um motorista por id
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <returns>Motorista</returns>
        Motorista ObterPorId(int id);

        /// <summary>
        /// Retorna todos os motoristas cadastrados
        /// </summary>
        /// <returns>Lista de Motoristas</returns>
        IEnumerable<Motorista> ListarTodos();

        /// <summary>
        /// Incluí o cadastro de um motorista
        /// </summary>
        /// <param name="motorista">Motorista que será incluído</param>
        void Incluir(Motorista motorista);

        /// <summary>
        /// Altera o cadastro de um motorista
        /// </summary>
        /// <param name="motorista">Motorista de será alterado</param>
        bool Alterar(Motorista motorista);

        /// <summary>
        /// Exclui o cadastro de um motorista
        /// </summary>
        /// <param name="id">Id do motorista a ser excluído</param>
        /// <returns>Motorista foi excluído com sucesso?</returns>
        bool Excluir(int id);
    }
}
