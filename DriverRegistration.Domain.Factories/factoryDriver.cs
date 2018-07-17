using DriverRegistration.Domain.Entities;
using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Factories
{
    public static class factoryDriver
    {
        #region Methods
        public static IDriver Create()
        {
            return new Driver();
        }

        public static IDriver Create(int id, string firstName, string lastName)
        {
            return new Driver()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };
        }

        public static IDriver Create(int id, string firstName, string lastName, IAddress address, ICar car)
        {
            return new Driver()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                DriverCar = car
            };
        }
        #endregion
    }
}
