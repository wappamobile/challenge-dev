using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WappaMobile.Domain;

namespace WappaMobile.Persistence
{
    public class DriverContext : DbContext
    {
        public DriverContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
