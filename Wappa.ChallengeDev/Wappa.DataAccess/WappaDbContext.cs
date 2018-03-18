using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Wappa.Models;

namespace Wappa.DataAccess
{
    public class WappaDbContext : DbContext
    {

        public WappaDbContext(DbContextOptions<WappaDbContext> options)
            : base(options) { }

        public DbSet<Taxista> Taxistas { get; set; }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WappaDbContext>
    {
        public WappaDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();

            var builder = new DbContextOptionsBuilder<WappaDbContext>();

            var connectionString = config["ConnectionStrings:EF"];

            builder.UseSqlServer(connectionString);

            return new WappaDbContext(builder.Options);
        }
    }
}
