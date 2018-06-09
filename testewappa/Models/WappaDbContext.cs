using Microsoft.EntityFrameworkCore;

namespace testewappa.Models
{
    public class WappaDbContext : DbContext
    {
        public WappaDbContext(DbContextOptions<WappaDbContext> options) : base(options)
        {
            
        }

        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}