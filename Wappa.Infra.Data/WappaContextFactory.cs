using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Wappa.Infra.Data.Context;

namespace Wappa.Infra.Data
{
    public class WappaContextFactory : IDesignTimeDbContextFactory<WappaContext>
    {
        private readonly IHostingEnvironment _env;

        public WappaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WappaContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TravelOffer;Trusted_Connection=True;MultipleActiveResultSets=true");                   
             
            return new WappaContext(_env);
        }
    }
}
