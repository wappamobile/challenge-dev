using DriverRegistration.Domain.Entities;
using DriverRegistration.Domain.Repositories.Interfaces;
using DriverRegistration.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverRegistration.Domain.Services
{
    public class DriverService : IDriverService
    {    
        private readonly IDriverRepository _driverRepository;
        private readonly IMapsService _mapsService;

        public DriverService(IDriverRepository driverRepository, 
                             IMapsService mapsService)
        {
            _driverRepository = driverRepository;
            _mapsService = mapsService;
        }

        public Driver Get(string id)
        {
            return _driverRepository.Get(id);
        }

        public List<Driver> GetAll(bool orderByDesc, bool byLastName)
        {
            var driverList = _driverRepository.GetAll();

            if (orderByDesc)
            {
                if (byLastName)
                    driverList = driverList.OrderByDescending(x => x.LastName).ToList();
                else
                    driverList = driverList.OrderByDescending(x => x.FirstName).ToList();
            }
            else
            {
                if (byLastName)
                    driverList = driverList.OrderBy(x => x.LastName).ToList();
                else
                    driverList = driverList.OrderBy(x => x.FirstName).ToList();
            }

            return driverList;
        }

        public async Task<Driver> Insert(Driver driver)
        {
            var coords = await _mapsService.GetCoordinates(driver.FullAddress);
            
            driver.Address.MapsLatitude = coords.Results.FirstOrDefault().Geometry.Location.Lat;
            driver.Address.MapsLongitude = coords.Results.FirstOrDefault().Geometry.Location.Lng;

            return await _driverRepository.Insert(driver);
        }

        public async Task Update(string id, Driver driver)
        {
            if (driver.Id is null)
                driver.Id = id;

            var coords = await _mapsService.GetCoordinates(driver.FullAddress);

            driver.Address.MapsLatitude = coords.Results.FirstOrDefault().Geometry.Location.Lat;
            driver.Address.MapsLongitude = coords.Results.FirstOrDefault().Geometry.Location.Lng;

            await _driverRepository.Update(id, driver);
        }

        public async Task Delete(Driver driver)
        {
            await _driverRepository.Delete(driver);
        }

        public async Task Delete(string id)
        {
            await _driverRepository.Delete(id);
        }
    }
}
