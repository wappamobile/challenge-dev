using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Model;
using MongoDB.Driver;

namespace WappaMobile.Driver.API.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DriverContext _context;

        public DriverRepository(IOptions<DriverSettings> settings)
        {
            _context = new DriverContext(settings);
        }

        public List<DriverRegistry> Get()
        {
            return _context.Driver.Find(_ => true).ToList();
        }

        public DriverRegistry Get(string id)
        {
            var filter = Builders<DriverRegistry>.Filter.Eq("Id", id);
            return _context.Driver
                .Find(filter)
                .FirstOrDefault();
        }

        public void Add(DriverRegistry driver)
        {
            _context.Driver.InsertOne(driver);
        }

        public void Update(DriverRegistry driver)
        {
            _context.Driver.ReplaceOne(
                doc => doc.Id == driver.Id,
                driver,
                new UpdateOptions { IsUpsert = true });
        }

        public void Delete(string id)
        {
            _context.Driver.DeleteOne(
                doc => doc.Id == id
            );
        }
    }
}
