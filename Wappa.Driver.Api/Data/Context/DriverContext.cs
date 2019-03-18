
using Microsoft.EntityFrameworkCore;
using Wappa.Driver.Api.Data.Models;

namespace Wappa.Driver.Api.Data.Context
{
    public class DriverContext : DbContext
    {
        private const string DatabaseSchema = "dbo";
        public DbSet<Models.Driver> Drivers { get; set; }
        public DbSet<DriverCar> DriverCars { get; set; }
        public DbSet<DriverAddress> DriverAddresses { get; set; }

        public DriverContext(DbContextOptions dbOptions) : base(dbOptions)
        {
        }

        #region Mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Driver>(entity =>
            {
                entity.HasKey(e => e.DriverID).HasName("PK_Driver");
                entity.ToTable("Driver", DatabaseSchema);
                entity.Property(e => e.DriverID).HasColumnType("int").IsRequired().UseSqlServerIdentityColumn();
                entity.Property(e => e.FirstName).IsRequired().HasColumnType("varchar(15)");
                entity.Property(e => e.LastName).IsRequired().HasColumnType("varchar(30)");
            });


            modelBuilder.Entity<DriverCar>(entity =>
            {
                entity.HasKey(e => e.CarId).HasName("PK_DriverCar");
                entity.ToTable("DriverCar", DatabaseSchema);
                entity.Property(e => e.CarId).HasColumnType("int").IsRequired().UseSqlServerIdentityColumn();
                entity.Property(e => e.DriverId).HasColumnType("int").IsRequired().ValueGeneratedNever();
                entity.Property(e => e.Make).IsRequired().HasColumnType("varchar(15)");
                entity.Property(e => e.Model).IsRequired().HasColumnType("varchar(15)");
                entity.Property(e => e.Plate).IsRequired().HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<DriverAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId).HasName("PK_DriverAddress");
                entity.ToTable("DriverAddress", DatabaseSchema);
                entity.Property(e => e.AddressId).HasColumnType("int").IsRequired().UseSqlServerIdentityColumn();
                entity.Property(e => e.DriverId).HasColumnType("int").IsRequired().ValueGeneratedNever();
                entity.Property(e => e.Address).IsRequired().HasColumnType("varchar(40)");
                entity.Property(e => e.Number).IsRequired().HasColumnType("varchar(10)");
                entity.Property(e => e.Neighborhood).IsRequired().HasColumnType("varchar(40)");
                entity.Property(e => e.City).IsRequired().HasColumnType("varchar(40)");
                entity.Property(e => e.State).IsRequired().HasColumnType("varchar(20)");
                entity.Property(e => e.Country).IsRequired().HasColumnType("varchar(20)");
                entity.Property(e => e.Latitude).IsRequired().HasColumnType("varchar(20)");
                entity.Property(e => e.Longitude).IsRequired().HasColumnType("varchar(20)");

            });
        }
        #endregion
    }
}
