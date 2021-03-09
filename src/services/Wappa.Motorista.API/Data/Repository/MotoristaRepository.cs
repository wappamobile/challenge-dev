using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wappa.Core.Data;
using Wappa.Motoristas.API.Models;
using Wappa.Motoristas.API.Data;

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
        
        public void Adicionar(Motorista cliente)
        {
            _context.Motorista.Add(cliente);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}