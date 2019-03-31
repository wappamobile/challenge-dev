using System;
using System.Collections.Generic;
using System.Text;
using MG.WappaDriverAPI.Core.Data.Interfaces;
using MG.WappaDriverAPI.Core.Models;
using MongoDB.Driver;

namespace MG.WappaDriverAPI.Core.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly MongoClient _client;
        private const string DatabaseName = "DriverStore";
        private const string CollectionName = "DriversAddresses";

        public AddressRepository(MongoClient client)
        {
            _client = client;
        }

        public Address SaveOrUpdateAddress(Address address)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            try
            {
                var addresses = session.Client.GetDatabase(DatabaseName)
                    .GetCollection<Address>(CollectionName);

                if (address.Id != new MongoDB.Bson.ObjectId())
                {
                    address.ModifiedAt = DateTime.UtcNow;
                    addresses.ReplaceOne(d => d.Id == address.Id, address);
                }
                else
                {
                    address.CreatedAt = DateTime.UtcNow;
                    addresses.InsertOne(address);
                }
                session.CommitTransaction();
                return addresses.FindSync(d => d.Id == address.Id).First();
            }
            catch (Exception ex)
            {
                session.AbortTransaction();
                throw ex;
            }
        }

        public void DeleteAddress(string addressId)
        {
            var session = _client.StartSession();
            session.StartTransaction();

            try
            {
                var addresses = session.Client.GetDatabase(DatabaseName)
                    .GetCollection<Address>(CollectionName);
                addresses.DeleteOne(d => d.Id == new MongoDB.Bson.ObjectId(addressId));
                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.AbortTransaction();
                throw ex;
            }
        }

        public Address GetAddressById(string addressId)
        {
            var session = _client.StartSession();
            var addresses = session.Client.GetDatabase(DatabaseName).GetCollection<Address>(CollectionName);
            return addresses.FindSync(d => d.Id == new MongoDB.Bson.ObjectId(addressId)).First();
        }

        public IEnumerable<Address> GetAddressesByDriverId(string driverId)
        {
            var session = _client.StartSession();
            var addresses = session.Client.GetDatabase(DatabaseName).GetCollection<Address>(CollectionName);
            return addresses.FindSync(d => d.DriverId.Equals(driverId)).ToList();
        }
    }
}
