using DriverRegistration.Application.DTOs.Driver;
using DriverRegistration.Application.Mappers.Driver;
using DriverRegistration.Domain.Interfaces;
using DriverRegistration.Domain.Services;
using DriverRegistration.InfraStructure.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DriverRegistration.Application
{
    public class ApplicationDriver
    {
        #region Constructors
        public ApplicationDriver(IConfiguration configuration)
        {
            _configuration = configuration;
            _repositoryDriver = new RepositoryDriver(_configuration);
            _serviceDriver = new ServiceDriver(_repositoryDriver);
        }
        #endregion

        #region Attributes
        private readonly IServiceDriver _serviceDriver;
        private readonly IRepositoryDriver _repositoryDriver;
        private readonly IConfiguration _configuration;
        #endregion

        #region Properties

        #endregion

        #region Methods
        public DriverResponse Add(DriverPostRequest request)
        {
            return MapperDriver.ParseToDTO(_serviceDriver.Add(MapperDriver.ParseToEntity(request)));
        }

        public bool Update(DriverPutRequest request)
        {
            return _serviceDriver.Update(MapperDriver.ParseToEntity(request));
        }

        public bool Delete(int id)
        {
            return _serviceDriver.Delete(id);
        }

        public DriverResponse Load(int id)
        {
            return MapperDriver.ParseToDTO(_serviceDriver.Load(id));
        }


        public IEnumerable<DriverResponse> Get(int rowindex, int rowget, int direction)
        {
            if (direction == 0)
                return MapperDriver.ParseToDTO(_serviceDriver.GetOrderByFirstName(rowindex, rowget));
            else if (direction == 1)
                return MapperDriver.ParseToDTO(_serviceDriver.GetOrderByLasttName(rowindex, rowget));
            else
                return null;
        }
        #endregion
    }
}
