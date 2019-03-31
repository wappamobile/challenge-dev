using System;
using System.Collections.Generic;
using System.Text;
using MG.WappaDriverAPI.Core.Data.Interfaces;
using MG.WappaDriverAPI.Core.Models;
using MongoDB.Driver;

namespace MG.WappaDriverAPI.Core.Data.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly MongoClient _client;
        private const string DatabaseName = "DriverStore";
        private const string CollectionName = "Drivers";

        public DriverRepository(MongoClient client)
        {
            _client = client;
        }

        public Driver GetById(string id)
        {
            var session = _client.StartSession();
            var drivers = session.Client.GetDatabase(DatabaseName).GetCollection<Driver>(CollectionName);
            return drivers.FindSync(d => d.Id == new MongoDB.Bson.ObjectId(id)).First();
        }

        public IEnumerable<Driver> FindByName(string firstName, string lastName)
        {
            var session = _client.StartSession();
            var drivers = session.Client.GetDatabase(DatabaseName).GetCollection<Driver>(CollectionName);
            return drivers.FindSync(d => d.FirstName.ToLower()==firstName.ToLower() && d.LastName.ToLower()==lastName.ToLower()).ToList();
        }

        public IEnumerable<Driver> FindByName(string name)
        {
            var session = _client.StartSession();
            var drivers = session.Client.GetDatabase(DatabaseName).GetCollection<Driver>(CollectionName);
            return drivers.FindSync(d => d.FirstName.ToLower().Contains(name.ToLower()) || d.LastName.ToLower().Contains(name.ToLower())).ToList();
        }

        public Driver SaveOrUpdate(Driver driver)
        {
            var session = _client.StartSession();

            session.StartTransaction();

            try
            {
                var drivers = session.Client.GetDatabase(DatabaseName).GetCollection<Driver>(CollectionName);
                if (driver.Id != new MongoDB.Bson.ObjectId())
                {
                    driver.ModifiedAt=DateTime.UtcNow;
                    drivers.ReplaceOne(d => d.Id == driver.Id, driver);
                }
                else
                {
                    driver.CreatedAt = DateTime.UtcNow;
                    drivers.InsertOne(driver);
                }

                return drivers.FindSync(d => d.Id == driver.Id).First();
            }
            catch (Exception ex)
            {
                session.AbortTransaction();
                throw ex;
            }
        }


        public Car GetCarByDriverId(string driverId)
        {
            var session = _client.StartSession();
            var drivers = session.Client.GetDatabase(DatabaseName).GetCollection<Driver>(CollectionName);
            return drivers.FindSync(d => d.Id == new MongoDB.Bson.ObjectId(driverId)).First().Car;
        }

        public void DeleteDriver(string driverId)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            try
            {
                var drivers = session.Client.GetDatabase(DatabaseName).GetCollection<Driver>(CollectionName);
                drivers.DeleteOne(d => d.Id == new MongoDB.Bson.ObjectId(driverId));
                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.AbortTransaction();
                throw ex;
            }
        }
    }
}
