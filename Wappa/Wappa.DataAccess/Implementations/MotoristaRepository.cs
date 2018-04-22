using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.DataAccess.Interfaces;
using Wappa.Models;

namespace Wappa.DataAccess.Implementations
{

    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly WappaContext context;

        public MotoristaRepository(WappaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Motorista> ListarTodos()
        {
            return context
                .Motoristas
                .Include(m => m.Carro)
                .Include(m => m.Endereco)
                .ToList();
        }

        public Motorista ObterPorId(int id)
        {
            return context
                .Motoristas
                .Include(m => m.Carro)
                .Include(m => m.Endereco)
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Motorista> ListarPorNome(string nome)
        {
            return context
                .Motoristas
                .Include(m => m.Carro)
                .Include(m => m.Endereco)
                .Where(m => m.Nome.Contains(nome))
                .ToList();
        }

        public IEnumerable<Motorista> ListarPorUltimoNome(string ultimoNome)
        {
            return context
                .Motoristas
                .Include(m => m.Carro)
                .Include(m => m.Endereco)
                .Where(m => m.UltimoNome.Contains(ultimoNome))
                .ToList();
        }

        public void Incluir(Motorista motorista)
        {
            context.Add(motorista);
            context.SaveChanges();
        }

        public bool Alterar(Motorista motorista)
        {
            var motoristaExiste = context.Motoristas.AsNoTracking().Any(m => m.Id == motorista.Id);

            if (!motoristaExiste)
            {
                return false;
            }

            context.Update(motorista);
            context.SaveChanges();

            return true;
        }

        public bool Excluir(int id)
        {
            var motorista = context.Motoristas.AsNoTracking().FirstOrDefault(m => m.Id == id);

            if (motorista == null)
            {
                return false;
            }

            context.Remove(motorista);
            context.SaveChanges();

            return true;
        }
    }
}