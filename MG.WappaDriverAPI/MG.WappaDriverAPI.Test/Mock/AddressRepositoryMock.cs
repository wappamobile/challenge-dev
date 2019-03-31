using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MG.WappaDriverAPI.Core.Data.Interfaces;
using MG.WappaDriverAPI.Core.Models;
using MongoDB.Bson;

namespace MG.WappaDriverAPI.Test.Data
{
    public class AddressRepositoryMock : IAddressRepository
    {
        private List<Address> _addresses;

        public AddressRepositoryMock()
        {
            _addresses = new List<Address>();
        }

        public Address SaveOrUpdateAddress(Address address)
        {
            address.Id=ObjectId.GenerateNewId();
            _addresses.Add(address);
            return address;
        }

        public void DeleteAddress(string addressId)
        {
            var driver = _addresses.FirstOrDefault(a => a.Id == new ObjectId(addressId));
            if (driver != null)
            {
                _addresses.Remove(driver);
            }
        }

        public Address GetAddressById(string addressId)
        {
            return _addresses.FirstOrDefault(a => a.Id == new ObjectId(addressId));
        }

        public IEnumerable<Address> GetAddressesByDriverId(string driverId)
        {
            return _addresses.Where(a => a.DriverId == driverId);
        }
    }
}
