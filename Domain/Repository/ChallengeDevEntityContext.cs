using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class ChallengeDevEntityContext : DbContext
    {
        public ChallengeDevEntityContext(DbContextOptions<ChallengeDevEntityContext> options)
            : base(options)
        { }

        public ChallengeDevEntityContext() : this(new DbContextOptions<ChallengeDevEntityContext>())
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=ChallengeDev.db", d => d.MigrationsAssembly("ChallengeDev"));
        }

        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
    }
}
