using System;

namespace DriverRegistration.Domain.Interfaces
{
    public interface IServiceCar
    {
        ICar Add(ICar car, int DriverId);

        bool Update(ICar car);

        Boolean Delete(int id);

        ICar Load(int DriverId);
    }
}
