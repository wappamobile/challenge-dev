using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.DataAccess
{
    public class WappaDbContextFactory : IDesignTimeDbContextFactory<WappaContext>
    {
        public WappaContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WappaContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile(@"appsettings.json")
              .Build();

            builder.UseSqlServer(configuration.GetConnectionString("WappaConnectionString"));
            return new WappaContext(builder.Options);
        }
    }
}
