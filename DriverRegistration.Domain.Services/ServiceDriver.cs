using DriverRegistration.Domain.Interfaces;
using System.Collections.Generic;

namespace DriverRegistration.Domain.Services
{
    public class ServiceDriver: IServiceDriver
    {
        #region Constructors
        public ServiceDriver(IRepositoryDriver repositoryDriver)
        {
            _repositoryDriver = repositoryDriver;
        }
        #endregion

        #region Attributes
        private readonly IRepositoryDriver _repositoryDriver;
        #endregion

        #region Properties

        #endregion

        #region Methods
        public IDriver Add(IDriver driver)
        {
            return _repositoryDriver.Add(driver);
        }

        public bool Update(IDriver driver)
        {
            return _repositoryDriver.Update(driver);
        }

        public bool Delete(int id)
        {
            return _repositoryDriver.Delete(id);
        }

        public IDriver Load(int id)
        {
            return _repositoryDriver.Load(id);
        }

        public IEnumerable<IDriver> GetOrderByFirstName(int rowindex, int rowget)
        {
            return _repositoryDriver.GetOrderByFirstName(rowindex, rowget);
        }

        public IEnumerable<IDriver> GetOrderByLasttName(int rowindex, int rowget)
        {
            return _repositoryDriver.GetOrderByLasttName(rowindex, rowget);
        }
        #endregion
    }
}
