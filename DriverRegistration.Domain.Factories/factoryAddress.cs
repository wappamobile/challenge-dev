using DriverRegistration.Domain.Entities;
using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Factories
{
    public static class factoryAddress
    {
        #region Methods
        public static IAddress Create()
        {
            return new Address();
        }

        public static IAddress Create(int id, string addressName, int number, string neighborhood, string postalCode, string state, decimal longitude, decimal latitude, string city)
        {
            return new Address()
            {
                Id = id,
                AddressName = addressName,
                Latitude = latitude,
                Longitude = longitude,
                Neighborhood = neighborhood,
                Number = number,
                PostalCode = postalCode,
                State = state,
                City = city
            };
        }
        #endregion
    }
}
