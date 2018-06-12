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
        private static DriverContext ctx;

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            var config = applicationBuilder
                .ApplicationServices.GetRequiredService<IOptions<DriverSettings>>();

            ctx = new DriverContext(config);

            if (!ctx.Driver.Database.GetCollection<DriverRegistry>(nameof(DriverRegistry)).AsQueryable().Any())
            {
                //Seed
            }
        }
    }
}
