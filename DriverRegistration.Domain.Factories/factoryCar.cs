using DriverRegistration.Domain.Entities;
using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Factories
{
    public static class factoryCar
    {
        #region Methods
        public static ICar Create()
        {
            return new Car();
        }

        public static ICar Create(int id, string brand,string model, string plate)
        {
            return new Car()
            {
                Id = id, 
                Brand = brand,
                Model = model,
                Plate = plate
            };
        }
        #endregion  
    }
}
