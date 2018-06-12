using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Model;
using MongoDB.Driver;

namespace WappaMobile.Driver.API.Infrastructure
{
    public class DriverContextSeed
    {
        private static DriverContext _context;

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            var config = applicationBuilder
                .ApplicationServices.GetRequiredService<IOptions<DriverSettings>>();

            _context = new DriverContext(config);

            if (!_context.Driver.Database.GetCollection<DriverRegistry>(nameof(DriverRegistry)).AsQueryable().Any())
            {
                //Seed
            }
        }
    }
}
