using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using MG.WappaDriverAPI.Core.Data.Interfaces;
using MG.WappaDriverAPI.Core.Models;
using MG.WappaDriverAPI.Core.Services.Interfaces;
using MG.WappaDriverAPI.Core.Validators;

namespace MG.WappaDriverAPI.Core.Services
{
    public class DriverService : IDriverService
    {
        private IDriverRepository _driverRepository;
        private IAddressRepository _addressRepository;
        private DriverValidator _driverValidator;
        private IGoogleApiMapsService _googleApiMapsService;
        private AddressValidator _addressValidator;

        public DriverService(IDriverRepository driverRepository, IAddressRepository addressRepository, DriverValidator driverValidator, IGoogleApiMapsService googleApiMapsService, AddressValidator addressValidator)
        {
            _driverRepository = driverRepository;
            _addressRepository = addressRepository;
            _driverValidator = driverValidator;
            _googleApiMapsService = googleApiMapsService;
            _addressValidator = addressValidator;
        }

        public Driver GetFullDriverById(string driverId)
        {
            if (!_driverValidator.ValidObjectId(driverId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }

            var drive= _driverRepository.GetById(driverId);

            drive.Addresses = _addressRepository.GetAddressesByDriverId(driverId);

            return drive;
        }

        public IEnumerable<Driver> FindByName(string firstName, string lastName)
        {
            return _driverRepository.FindByName(firstName, lastName);
        }

        public IEnumerable<Driver> FindByName(string name)
        {
            return _driverRepository.FindByName(name);
        }

        public Driver SaveOrUpdate(Driver driver)
        {
            ValidationResult results = _driverValidator.Validate(driver);
            if (!results.IsValid)
            {
                throw new ArgumentException(string.Join("\n\r", results.Errors.Select(a => a.ErrorMessage)));
            }

            return _driverRepository.SaveOrUpdate(driver);
        }

        public IEnumerable<Address> GetAddressesByDriverId(string driverId)
        {
            if (!_driverValidator.ValidObjectId(driverId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }
            return _addressRepository.GetAddressesByDriverId(driverId);
        }

        public Car GetCarByDriverId(string driverId)
        {
            if (!_driverValidator.ValidObjectId(driverId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }

            return _driverRepository.GetCarByDriverId(driverId);
        }

        public Address SaveDriverAddress(string driverId, string name, string address)
        {
            if (!_driverValidator.ValidObjectId(driverId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }
            
            if (address == null) throw new ArgumentNullException(nameof(address));
            var result = _googleApiMapsService.GetAddressFromGoogle(address);
            var objaddress = result.ToAddress();
            objaddress.Name = name;
            objaddress.DriverId = driverId;

            ValidationResult results = _addressValidator.Validate(objaddress);
            if (!results.IsValid)
            {
                throw new ArgumentException(string.Join("\n\r", results.Errors.Select(a => a.ErrorMessage)));
            }

            return _addressRepository.SaveOrUpdateAddress(objaddress);
        }

        public void DeleteDriverAddress(string addressId)
        {
            if (!_driverValidator.ValidObjectId(addressId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }

            _addressRepository.DeleteAddress(addressId);
        }

        public Address GetDriverAddressById(string addressId)
        {
            if (!_driverValidator.ValidObjectId(addressId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }
            return _addressRepository.GetAddressById(addressId);
        }

        public Driver GetDriverById(string driverId)
        {
            if (!_driverValidator.ValidObjectId(driverId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }
            return _driverRepository.GetById(driverId);
        }

        public void DeleteDriver(string driverId)
        {
            if (!_driverValidator.ValidObjectId(driverId))
            {
                throw new ArgumentException(string.Join("\n\r", "Invelid Driver Id "));
            }
            _driverRepository.DeleteDriver(driverId);
        }
    }
}
