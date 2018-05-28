using Microsoft.EntityFrameworkCore;
using Wappa.Infraestrutura.Mapeamento;

namespace Wappa.Infraestrutura.Contexto
{
    public class WappaContexto : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WappaBanco;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarroMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new MotoristaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}