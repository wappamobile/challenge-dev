using Wappa.Middleware.Domain.Cars;
using Wappa.Middleware.Domain.Drivers;
using Wappa.Middleware.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Wappa.Middleware.EntityFrameworkCore.Contexts
{
    public class AppDbContext : DbContext
    {
        public string UserId { get; set; }
        public int? TenantId { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.EnableSoftDelete();
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}
