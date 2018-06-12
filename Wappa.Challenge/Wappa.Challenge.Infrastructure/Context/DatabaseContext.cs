using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;
using Wappa.Challenge.ApplicationCore.Entities;
using Wappa.Challenge.Util.Auditoria;

namespace Wappa.Challenge.Infrastructure.Context
{
    public partial class DatabaseContext : DbContextPadrao
    {
        public DatabaseContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Util.Conexao.SqlServer.StringDeConexao);
        }

        public virtual DbSet<Motorista> Motorista { get; set; }
        public virtual DbSet<Carro> Carro { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
