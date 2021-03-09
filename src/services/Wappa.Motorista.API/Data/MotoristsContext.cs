using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wappa.Motoristas.API.Models;
using Wappa.Core.Data;
using Wappa.Core.DomainObjects;

namespace Wappa.Motoristas.API.Data
{
    public sealed class MotoristaContext : DbContext, IUnitOfWork
    {
        
        public MotoristaContext(DbContextOptions<MotoristaContext> options)
            : base(options)
        {            
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Carro> Carros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MotoristaContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;            

            return sucesso;
        }
    }    
}