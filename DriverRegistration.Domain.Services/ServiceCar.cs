using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Services
{
    public class ServiceCar: IServiceCar
    {
        #region Constructors
        public ServiceCar(IRepositoryCar repositoryCar)
        {
            _repositoryCar = repositoryCar;
        }
        #endregion

        #region Attributes
        private readonly IRepositoryCar _repositoryCar;
        #endregion

        #region Properties

        #endregion

        #region Methods
        public ICar Add(ICar car, int DriverId)
        {
            return _repositoryCar.Add(car, DriverId);
        }

        public bool Update(ICar car)
        {
            return _repositoryCar.Update(car);
        }

        public bool Delete(int id)
        {
            return _repositoryCar.Delete(id);
        }

        public ICar Load(int DriverId)
        {
            return _repositoryCar.Load(DriverId);
        }
        #endregion
    }
}
