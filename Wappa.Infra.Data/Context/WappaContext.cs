using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Wappa.Domain.Models;
using Wappa.Infra.Data.Mappings;

namespace Wappa.Infra.Data.Context
{
    [DbContext(typeof(WappaContext))]
    public class WappaContext : DbContext
    {
        private readonly IHostingEnvironment _env;

        public WappaContext(IHostingEnvironment env)
        {
            _env = env;
        }

        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DriverMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// get the configuration from the app settings
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(_env.ContentRootPath)
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //// define the database to use
            //optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wappa;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
