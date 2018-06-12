using System.Collections.Generic;
using System.Linq;
using testewappa.Models;

namespace testewappa.Repository
{
    public class WappaRepository : IWappaRepository
    {
        private WappaDbContext _context { get; set; }

        public WappaRepository(WappaDbContext context)
        {
            _context = context;
        }

        public void Add(Motorista motorista)
        {
            _context.Motoristas.Add(motorista);
            _context.SaveChanges();
        }

        public Motorista Find(int id)
        {
             return _context.Motoristas.FirstOrDefault(m => m.Id == id);
        }
        public void Delete(int id)
        {
             var entity = _context.Motoristas.First(m=> m.Id == id);
        }

        public IEnumerable<Motorista> GetAll(string order)
        {
            return order == "S" ? _context.Motoristas.ToList().OrderBy(m => m.Sobrenome) : _context.Motoristas.ToList().OrderBy(m => m.Nome);
        }

        public void Update(Motorista motorista)
        {
            _context.Motoristas.Update(motorista);
            _context.SaveChanges();
        }
    }
}