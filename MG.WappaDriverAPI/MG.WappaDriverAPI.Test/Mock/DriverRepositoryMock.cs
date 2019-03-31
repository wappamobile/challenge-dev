using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MG.WappaDriverAPI.Core.Data.Interfaces;
using MG.WappaDriverAPI.Core.Models;
using MongoDB.Bson;
using Xunit;

namespace MG.WappaDriverAPI.Test.Data
{
    public class DriverRepositoryMock : IDriverRepository
    {
        private List<Driver> _drivers;

        public DriverRepositoryMock()
        {
            _drivers = new List<Driver>();
        }

        public Driver GetById(string id)
        {
            return _drivers.FirstOrDefault(a => a.Id == new ObjectId(id));
        }

        public IEnumerable<Driver> FindByName(string firstName, string lastName)
        {
            return _drivers.Where(a => a.FirstName.Equals(firstName, StringComparison.InvariantCultureIgnoreCase) || a.LastName.Equals(lastName, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<Driver> FindByName(string name)
        {
            return _drivers.Where(a => a.FirstName.Contains(name, StringComparison.InvariantCultureIgnoreCase) || a.LastName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public Driver SaveOrUpdate(Driver driver)
        {
            driver.Id = ObjectId.GenerateNewId();
            _drivers.Add(driver);
            return driver;
        }

        public Car GetCarByDriverId(string driverId)
        {
            return _drivers.FirstOrDefault(a => a.Id == new ObjectId(driverId))?.Car;
        }

        public void DeleteDriver(string driverId)
        {
            var driver = _drivers.FirstOrDefault(a => a.Id == new ObjectId(driverId));
            if (driver != null)
            {
                _drivers.Remove(driver);
            }
        }
    }
}
