using System;
using Microsoft.EntityFrameworkCore;
using Wappa.Domain.Entities;

namespace Wappa.Domain.Concrete {
    class DbWappa: DbContext {
        
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=Wappa;User ID=sa;Password=S3nh@$@"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Modelo>().ToTable("Modelo");
            modelBuilder.Entity<Carro>().ToTable("Carro");
            modelBuilder.Entity<Endereco>().ToTable("Endereco");
            modelBuilder.Entity<Motorista>().ToTable("Motorista");

            modelBuilder.Entity<Modelo>()
            .HasOne(p => p.Marca);

            modelBuilder.Entity<Carro>()
            .HasOne(p => p.Modelo); 

            modelBuilder.Entity<Motorista>()
            .HasOne(p => p.Endereco);                        

            modelBuilder.Entity<Motorista>()
            .HasOne(p => p.Carro);

        }
    }
}