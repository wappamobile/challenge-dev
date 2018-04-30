using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Domain.Models;
using Wappa.Challenge.Domain.Repositories;

namespace Wappa.Challenge.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IMongoCollection<Driver> _collection;

        public DriverRepository(IMongoDatabase db)
        {
            _collection = db.GetCollection<Driver>("driver");
        }

        public void Insert(Driver driver)
        {
            _collection.InsertOne(driver);
        }

        public void Update(Driver driver)
        {
            var filter = Builders<Driver>.Filter.Eq(d => d.Id, driver.Id);
            _collection.ReplaceOne(filter, driver);
        }

        public void Delete(Guid driverId)
        {
            _collection.DeleteOne(d => d.Id == driverId);
        }

        public bool Exists(Guid driverId)
        {
            return _collection.Find(d => d.Id == driverId).Any();
        }

        public List<Driver> List(OrderByOptionCommand orderBy)
        {
            SortDefinition<Driver> sort;
            switch (orderBy)
            {
                case OrderByOptionCommand.Firstname:
                    sort = Builders<Driver>.Sort.Ascending(d => d.FirstName);
                    break;
                case OrderByOptionCommand.Lastname:
                    sort = Builders<Driver>.Sort.Ascending(d => d.FirstName);
                    break;
                default:
                    sort = Builders<Driver>.Sort.Ascending(d => d.Id);
                    break;
            }

            return _collection
                .Find(d => true)
                .Sort(sort)
                .ToList();
        }
    }
}