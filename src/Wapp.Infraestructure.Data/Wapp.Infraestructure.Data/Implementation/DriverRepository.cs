using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Infrastructure.Data.Interface;
using Wappa.Infrastructure.Data.Models;

namespace Wappa.Infrastructure.Data.Implementation
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IDriverContext _context;

        public DriverRepository(IDriverContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
            return await _context
                            .Drivers
                            .Find(_ => true)
                            .SortBy(f => f.FirstName)
                            .ToListAsync();
        }
        public Task<Driver> GetDriver(string id)
        {
            FilterDefinition<Driver> filter = Builders<Driver>.Filter.Eq(m => m.Id, id);
            return _context
                    .Drivers
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(Driver driver)
        {
            await _context.Drivers.InsertOneAsync(driver);
        }
        public async Task<bool> Update(Driver driver)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Drivers
                        .ReplaceOneAsync(
                            filter: g => g.Id == driver.Id,
                            replacement: driver);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Driver> filter = Builders<Driver>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Drivers
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<long> GetNextId()
        {
            return await _context.Drivers.CountDocumentsAsync(new BsonDocument()) + 1;
        }

    }
}
