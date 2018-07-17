using DriverRegistration.Application.DTOs.Car;
using DriverRegistration.Application.Mappers.Car;
using DriverRegistration.Domain.Interfaces;
using DriverRegistration.Domain.Services;
using DriverRegistration.InfraStructure.Repository;
using Microsoft.Extensions.Configuration;
using System;

namespace DriverRegistration.Application
{
    public class ApplicationCar
    {
        #region Constructors
        public ApplicationCar(IConfiguration configuration)
        {
            _configuration = configuration;
            _repositoryCar = new RepositoryCar(_configuration);
            _serviceCar = new ServiceCar(_repositoryCar);
        }
        #endregion

        #region Attributes
        private readonly IServiceCar _serviceCar;
        private readonly IRepositoryCar _repositoryCar;
        private readonly IConfiguration _configuration;
        #endregion

        #region Properties

        #endregion

        #region Methods
        public CarResponse Add(CarPostRequest request)
        {
            return MapperCar.ParseToDTO(_serviceCar.Add(MapperCar.ParseToEntity(request), request.DriverId));
        }

        public bool Update(CarPutRequest request)
        {
            return _serviceCar.Update(MapperCar.ParseToEntity(request));
        }

        public Boolean Delete(int id)
        {
            return _serviceCar.Delete(id);
        }

        public CarResponse Load(int DriverId)
        {
            return MapperCar.ParseToDTO(_serviceCar.Load(DriverId));
        }
        #endregion
    }
}
