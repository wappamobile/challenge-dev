using System;
using System.Collections.Generic;
using System.Text;
using MG.WappaDriverAPI.Core.Models;

namespace MG.WappaDriverAPI.Core.Data.Interfaces
{
    public interface IDriverRepository
    {
        Driver GetById(string id);
        IEnumerable<Driver> FindByName(string firstName, string lastName);
        IEnumerable<Driver> FindByName(string name);
        Driver SaveOrUpdate(Driver driver);
        Car GetCarByDriverId(string driverId);
        void DeleteDriver(string driverId);
    }
}
