using DriverRegistration.Domain.Entities;
using DriverRegistration.Domain.Repositories.Interfaces;
using DriverRegistration.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace DriverRegistration.Application
{
    public class DriverApplication
    {
        IDriverService _driverService;
        
        public DriverApplication(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public Driver Get(string id)
        {
            return _driverService.Get(id);
        }

        public List<Driver> GetAll(bool orderByDesc, bool byLastName)
        {
            return _driverService.GetAll(orderByDesc, byLastName);
        }

        public void Insert(Driver driver)
        {
            _driverService.Insert(driver);
        }

        public void Update(string id, Driver driver)
        {
            _driverService.Update(id, driver);
        }

        public void Delete(Driver driver)
        {
            _driverService.Delete(driver);
        }

        public void Delete(string id)
        {
            _driverService.Delete(id);
        }

    }
}
