using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Challenge.Domain.DriverAggregation;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Challenge.Infra.Mongo
{
    public class MongoDriverRepository : Domain.DriverAggregation.IDriverRepository
    {
        private readonly string connectionString;
        private const string databaseName = "DriverAggregation";

        public MongoDriverRepository(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            connectionString = configuration["MongoConnectionString"];
        }

        public async Task Add(Driver driver)
        {
            var collection = await GetCollection();
            await collection.InsertOneAsync(driver);
        }

        private async Task<MongoDB.Driver.IMongoCollection<Driver>> GetCollection()
        {
            var client = new MongoDB.Driver.MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<Driver>(nameof(Driver));
            return collection;
        }

        public async Task<Driver> GetById(IQueryableById queryableById)
        {
            var collection = await GetCollection();
            return await collection.AsQueryable().FirstOrDefaultAsync(d => d.Id == new MongoDB.Bson.ObjectId(queryableById.Id));
        }

        public async Task Remove(Driver driver)
        {
            var collection = await GetCollection();
            await collection.DeleteOneAsync(d=>d.Id == driver.Id);
        }

        public async Task Update(Driver driver)
        {
            var collection = await GetCollection();
            await collection.ReplaceOneAsync(d => d.Id == driver.Id, driver);
        }

        public async Task<IEnumerable<Driver>> Get(GetDriversOrderType orderType)
        {
            var collection = await GetCollection();
            var query = collection.AsQueryable();
            switch (orderType)
            {
                case GetDriversOrderType.OrderByName:
                    query = query.OrderBy(d => d.FirstName);
                    break;
                    
                case GetDriversOrderType.OrderByLastName:
                    query = query.OrderBy(d => d.LastName);
                    break;
                
            }
            return await query.ToListAsync();
        }
    }
}
