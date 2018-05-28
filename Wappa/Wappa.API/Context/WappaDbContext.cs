using Microsoft.EntityFrameworkCore;
using Wappa.Entities;

namespace Wappa.API.Context
{
    public class WappaDbContext : DbContext
    {
        public WappaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
    }
}
