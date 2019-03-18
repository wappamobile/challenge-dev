using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Wappa.Driver.Api.Data.Context;

namespace Core.Company.Data.Context
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DriverContext>
    {
        public DriverContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DriverContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Driver;Integrated Security=True");

            return new DriverContext(optionsBuilder.Options);
        }
    }
}
