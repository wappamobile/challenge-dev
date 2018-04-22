using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;
using Wappa.Models.Enum;

namespace Wappa.Business.Interfaces
{
    public interface IMotoristaBusiness
    {
        /// <summary>
        /// Retorna um motorista
        /// </summary>
        /// <param name="id">Id do motorista</param>
        /// <returns></returns>
        Motorista ObterPorId(int id);

        /// <summary>
        /// Retorna os motoristas cadastrados
        /// </summary>
        /// <param name="ordenacao">Opção de Ordenação</param>
        /// <returns></returns>
        IEnumerable<Motorista> Listar(CampoOrdenacaoEnum? ordenacao);

        /// <summary>
        /// Salva um motorista
        /// </summary>
        /// <param name="motorista">Motorista incluído</param>
        Task Incluir(Motorista motorista);

        /// <summary>
        /// Altera um motorista
        /// </summary>
        /// <param name="motorista">Motorista a ser alterado</param>
        /// <returns>Indica se o id foi encontrado na base para ser alterado</returns>
        Task<bool> Alterar(Motorista motorista);

        /// <summary>
        /// Exclui um motorista
        /// </summary>
        /// <param name="id">Id do motorista a ser excluído</param>
        /// <returns>Indica se o id foi encontrado na base para ser excluído</returns>
        bool Excluir(int id);
    }
}
