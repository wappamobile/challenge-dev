using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Wappa.Infra.Data.Context;

namespace Wappa.Infra.Data
{
    public class EventStoreSQLContextFactory : IDesignTimeDbContextFactory<EventStoreSQLContext>
    {
        private readonly IHostingEnvironment _env;

        public EventStoreSQLContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventStoreSQLContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TravelOffer;Trusted_Connection=True;MultipleActiveResultSets=true");                   
             
            return new EventStoreSQLContext(_env);
        }
    }
}
