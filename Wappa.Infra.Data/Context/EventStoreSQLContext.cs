using System.IO;
using Wappa.Domain.Core.Events;
using Wappa.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace Wappa.Infra.Data.Context
{
    public class EventStoreSQLContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }
        private readonly IHostingEnvironment _env;

        public EventStoreSQLContext(IHostingEnvironment env)
        {
            _env = env;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            // define the database to use
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wappa;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}