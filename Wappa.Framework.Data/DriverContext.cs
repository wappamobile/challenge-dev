using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Framework.Model.Comum;
using Wappa.Framework.Model.Pessoa;
using Wappa.Framework.Model.Veiculo;

namespace Wappa.Framework.Data
{
    public class DriverContext : DbContext
    {
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Carro> Carros { get; set; }

        public DriverContext(DbContextOptions<DriverContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorista>().HasKey(c => c.PessoaId);
            modelBuilder.Entity<Endereco>().HasKey(c => c.EnderecoId);
            modelBuilder.Entity<Carro>().HasKey(c => c.VeiculoId);
        }
    }
}
