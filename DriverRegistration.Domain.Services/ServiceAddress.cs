using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Domain.Services
{
    public class ServiceAddress: IServiceAddress
    {
        #region Constructors
        public ServiceAddress(IRepositoryAddress repositoryAddress)
        {
            _repositoryAddress = repositoryAddress;
        }
        #endregion

        #region Attributes
        private readonly IRepositoryAddress _repositoryAddress;
        #endregion

        #region Properties

        #endregion

        #region Methods
        public IAddress Add(IAddress address, int DriverId)
        {
            return _repositoryAddress.Add(address, DriverId);
        }

        public bool Update(IAddress address)
        {
            return _repositoryAddress.Update(address);
        }

        public bool Delete(int id)
        {
            return _repositoryAddress.Delete(id);
        }

        public IAddress Load(int DriverId)
        {
            return _repositoryAddress.Load(DriverId);
        }
        #endregion
    }
}
