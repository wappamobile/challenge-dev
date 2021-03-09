using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Wappa.Core.Data;

namespace Wappa.Motoristas.API.Models
{
    public interface IMotoristaRepository : IRepository<Motorista>
    {
        void Adicionar(Motorista motorista);
        void Atualizar(Motorista motorista);
        void Deletar(Motorista motorista);

        Task<Motorista> ObterPorId(Guid id);
        Task<IEnumerable<Motorista>> ObterTodos();

        DbConnection ObterConexao();
    }
}