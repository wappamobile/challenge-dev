using System;
using System.Collections.Generic;
using System.Text;
using Cadastro.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Data
{
    public class AppDBContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;User Id=sa;Password=pl$150886;Database=DBChallengeDev;Integrated Security=True");
        }

        public DbSet<Motorista> Motoristas { get; set; }
        public  DbSet<Endereco> Enderecos { get; set; }
        public  DbSet<Coordenada> Coordenadas{ get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }
      
    }
}
