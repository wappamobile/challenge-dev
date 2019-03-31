using System.Collections.Generic;
using MG.WappaDriverAPI.Core.Models;

namespace MG.WappaDriverAPI.Core.Services.Interfaces
{
    public interface IDriverService
    {
        Driver GetDriverById(string driverId);
        Driver GetFullDriverById(string driverId);
        IEnumerable<Driver> FindByName(string firstName, string lastName);
        IEnumerable<Driver> FindByName(string name);
        Driver SaveOrUpdate(Driver driver);
        Car GetCarByDriverId(string driverId);
        IEnumerable<Address> GetAddressesByDriverId(string driverId);
        void DeleteDriverAddress(string addressId);
        Address GetDriverAddressById(string addressId);
        void DeleteDriver(string driverId);
        Address SaveDriverAddress(string driverId, string name, string address);
    }
}
