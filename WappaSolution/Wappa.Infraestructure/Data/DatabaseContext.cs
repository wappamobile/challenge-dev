using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Wappa.Domain.Entidades;

namespace Wappa.Infraestructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorista>().ToTable("Motorista");
            modelBuilder.Entity<Endereco>().ToTable("Endereco");
            modelBuilder.Entity<Veiculo>().ToTable("Veiculo");

            modelBuilder.Entity<Motorista>().HasOne(m => m.Endereco).WithOne();
            modelBuilder.Entity<Motorista>().HasOne(m => m.Veiculo).WithOne();

            base.OnModelCreating(modelBuilder);
        }
    }
}
