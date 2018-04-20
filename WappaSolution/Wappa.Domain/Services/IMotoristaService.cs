using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Domain.Entidades;

namespace Wappa.Domain.Services
{
    public interface IMotoristaService
    {
        /// <summary>
        /// Obtem um motorista pelo ID
        /// </summary>
        /// <param name="motoristaID">Id do motorista</param>
        /// <returns>Entidade Motorista</returns>
        Motorista ObterPorId(int motoristaID);

        /// <summary>
        /// Obter lista de motorista Ordenado pelo primeiro nome.
        /// </summary>
        /// <returns>Lista de Motorista</returns>
        IEnumerable<Motorista> ObterOrdenadoPorPrimeiroNome();

        /// <summary>
        /// Obter lista de motorista Ordenado pelo ultimo nome.
        /// </summary>
        /// <returns>Lista de Motorista</returns>
        IEnumerable<Motorista> ObterOrdenadoPorUltimoNome();

        /// <summary>
        /// Incluir Novo Motorista
        /// </summary>
        /// <param name="motorista">Entidade Motorista</param>
        void Novo(Motorista motorista);

        /// <summary>
        /// Alterar Motorista
        /// </summary>
        /// <param name="motorista">Entidade Motorista</param>
        void Alterar(Motorista motorista);

        /// <summary>
        /// Excluir motorista
        /// </summary>
        /// <param name="motoristaID">ID do Motorista</param>
        void Excluir(int motoristaID);
    }
}
