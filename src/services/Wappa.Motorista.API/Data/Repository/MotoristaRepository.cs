using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wappa.Core.Data;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Data.Repository
{
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly MotoristaContext _context;

        public MotoristaRepository(MotoristaContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        public DbConnection ObterConexao() => _context.Database.GetDbConnection();

        public async Task<IEnumerable<Motorista>> ObterTodos()
        {
            return await _context.Motorista.AsNoTracking().ToListAsync();
        }
        
        public void Adicionar(Motorista motorista)
        {
            _context.Motorista.Add(motorista);
        }


		public void Atualizar(Motorista motorista)
		{
            _context.Motorista.Update(motorista);
		}

		public void Deletar(Motorista motorista)
		{
            _context.Motorista.Remove(motorista);
		}

		public Task<Motorista> ObterPorId(Guid id)
		{
            return _context.Motorista
                .Include(d => d.Carro)
                .Include(f=>f.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);
		}

        public void Dispose()
        {
            _context.Dispose();
        }

	}
}