using System.Collections.Generic;
using MG.WappaDriverAPI.Core.Models;

namespace MG.WappaDriverAPI.Core.Data.Interfaces
{
    public interface IAddressRepository
    {
        Address SaveOrUpdateAddress(Address address);
        void DeleteAddress(string addressId);
        Address GetAddressById(string addressId);
        IEnumerable<Address> GetAddressesByDriverId(string driverId);
    }
}