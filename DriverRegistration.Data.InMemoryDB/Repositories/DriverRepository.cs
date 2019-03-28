using DriverRegistration.Data.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using DriverRegistration.Domain.Repositories.Interfaces;
using System.Data;
using DriverRegistration.Domain.Entities;
using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DriverRegistration.Data.MongoDb.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        const string MONGO_DB_CONNECTION = "mongodb://localhost:27017";

        private readonly IMongoCollection<Driver> _drivers;

        public DriverRepository()
        {
            var client = new MongoClient(MONGO_DB_CONNECTION);
            var database = client.GetDatabase("DAM");
            _drivers = database.GetCollection<Driver>("Drivers");
        }

        public Driver Get(string id)
        {
            return _drivers.Find(d => d.Id == id).FirstOrDefault();
        }

        public List<Driver> GetAll()
        {
            return _drivers.Find(d => true).ToList();
        }

        public async Task<Driver> Insert(Driver driver)
        {
            await _drivers.InsertOneAsync(driver);
            return driver;
        }

        public async Task Update(string id, Driver driver)
        {
            await _drivers.ReplaceOneAsync(d => d.Id == id, driver);
        }

        public async Task Delete(Driver driver)
        {
            await _drivers.DeleteOneAsync(d => d.Id == driver.Id);
        }

        public async Task Delete(string id)
        {
            await _drivers.DeleteOneAsync(d => d.Id == id);
        }
    }
}
