using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Model;
using MongoDB.Driver;
using WappaMobile.Driver.Infrastructure;

namespace WappaMobile.Driver.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IDbContext _context;

        public DriverRepository(IDbContext context)
        {
            _context = context;
        }

        public List<DriverRegistry> Get()
        {
            return _context.Driver.Find(_ => true).ToList();
        }

        public List<DriverRegistry> GetPendingGeolocation()
        {
            var filter = Builders<DriverRegistry>.Filter.Eq("FetchGeolocation", true);
            return _context.Driver.Find(filter).ToList();
        }

        public DriverRegistry Get(string id)
        {
            var filter = Builders<DriverRegistry>.Filter.Eq("Id", id);
            return _context.Driver
                .Find(filter)
                .FirstOrDefault();
        }

        public bool Add(DriverRegistry driver)
        {
            _context.Driver.InsertOne(driver);

            return true;
        }

        public bool Update(DriverRegistry driver)
        {
            _context.Driver.ReplaceOne(
                doc => doc.Id == driver.Id,
                driver,
                new UpdateOptions { IsUpsert = false }
            );

            return true;
        }

        public bool Delete(string id)
        {
            _context.Driver.DeleteOne(
                doc => doc.Id == id
            );

            return true;
        }
    }
}
