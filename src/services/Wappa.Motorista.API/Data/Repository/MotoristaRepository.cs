using System.Collections.Generic;
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

        public void Dispose()
        {
            _context.Dispose();
        }
	}
}