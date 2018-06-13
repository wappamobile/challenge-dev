using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Model;
using MongoDB.Driver;
using WappaMobile.Driver.Infrastructure;

namespace WappaMobile.Driver.API.Infrastructure
{
    public class DriverContextSeed
    {
        private static IDbContext _context;

        public static void Seed(IDbContext context)
        {
            _context = context;

            if (!_context.Driver.Database.GetCollection<DriverRegistry>("Driver").AsQueryable().Any())
            {
                _context.Driver.InsertOne(
                    new DriverRegistry {
                        Name = new FullName { FirstName = "Caio", LastName = "Zem" },
                        Vehicle = new Vehicle { Brand = "GM", Model = "CHEVROLET ONIX HATCH LTZ 1.4 8V FLEXPOWER 5P", LicensePlate = "XXX-9999" },
                        Address = "R. Gomes de Carvalho",
                        FetchGeolocation = true,
                        LastUpdated = DateTime.UtcNow
                    }
                );
            }
        }
    }
}
