using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Entities
{
    public class Driver: IDriver
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICar DriverCar { get; set; }

        public IAddress Address { get; set; }
        #endregion
    }
}
