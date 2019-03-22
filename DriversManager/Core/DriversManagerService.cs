using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using DriversManager.Core.Entities;
using System.Net.Http;

namespace DriversManager.Core
{
    public class DriversManagerService
    {
        private readonly IMongoCollection<Driver> _drivers;

        public DriversManagerService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("driversManagerDb"));
            var database = client.GetDatabase("DriversManager");
            _drivers = database.GetCollection<Driver>("Driver");
        }

        public List<Driver> Get()
        {
            return _drivers.Find(driver => true).ToList();
        }

        public List<Driver> GetOrderedByName()
        {
            return _drivers.Find(driver => true).ToList().OrderBy(x => x.Name).ToList();
        }

        public List<Driver> GetOrderedByLastName()
        {
            return _drivers.Find(driver => true).ToList().OrderBy(x => x.LastName).ToList();
        }
        public Driver Get(string id)
        {
            return _drivers.Find<Driver>(drive => drive.Id == long.Parse(id)).FirstOrDefault();
        }

        public Driver Create(Driver driverIn)
        {
            //GetLatLng From google
            driverIn = GetDataFromGoogleAPI (driverIn);
            
            _drivers.InsertOne(driverIn);
            return driverIn;
        }

        private Driver GetDataFromGoogleAPI(Driver driver)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //                        new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var url = "http://maps.google.com/maps/geo?q=" + driver.Street + " " + driver.Number + " " + driver.City;
            var buffer = client.GetStringAsync(url).GetAwaiter().GetResult();

            if (buffer != null && buffer != "")
            {
                string[] coord = buffer.Split(',');
                if (coord.Length >= 4)
                {
                    driver.Lat = coord[2].Replace(".", ",");
                    driver.Lng = coord[3].Replace(".", ",");
                 }
            }
            return driver;
        }

        public void Update(string id, Driver driverIn)
        {
            _drivers.ReplaceOne(driver => driver.Id == long.Parse(id), driverIn);
        }

        public void Remove(Driver driverIn)
        {
            _drivers.DeleteOne(driver => driver.Id == driverIn.Id);
        }

        public void Remove(string id)
        {
            _drivers.DeleteOne(driver => driver.Id == long.Parse(id));
        }
    }
}