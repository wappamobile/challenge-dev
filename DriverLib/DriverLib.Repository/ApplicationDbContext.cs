using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DriverLib.Domain;
using DriverLib.Repository.Mapping;

namespace DriverLib.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserMap(modelBuilder.Entity<User>());
            new AddressMap(modelBuilder.Entity<Address>());
            new JobHistoryMap(modelBuilder.Entity<JobHistory>());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.LogChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
