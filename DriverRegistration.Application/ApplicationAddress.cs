using DriverRegistration.Application.DTOs.Address;
using DriverRegistration.Application.Mappers.Address;
using DriverRegistration.Domain.Interfaces;
using DriverRegistration.Domain.Services;
using DriverRegistration.ExternalServices.GoogleAPI.Maps;
using DriverRegistration.InfraStructure.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DriverRegistration.Application
{
    public class ApplicationAddress
    {
        #region Constructors
        public ApplicationAddress(IConfiguration configuration)
        {
            _configuration = configuration;
            _repositoryAddress = new RepositoryAddress(_configuration);
            _serviceAddress = new ServiceAddress(_repositoryAddress);
            _ApiGeocoding = new GeocodingAPI(_configuration);
        }
        #endregion

        #region Attributes
        private readonly IServiceAddress _serviceAddress;
        private readonly IRepositoryAddress _repositoryAddress;
        private readonly IConfiguration _configuration;
        private readonly IExternalApiGeocoding _ApiGeocoding;
        #endregion

        #region Properties

        #endregion

        #region Methods
        public AddressResponse Add(AddressPostRequest request)
        {
            IAddress _request = MapperAddress.ParseToEntity(request);

            IDictionary<String, Decimal> _geo = _ApiGeocoding.GetGeoCordinates(request.AddressName + "," + request.Number + "," + request.City);

            if (_geo.ContainsKey("longitude") && _geo.ContainsKey("latitude"))
            {
                decimal _longitude = 0m;
                decimal _latitude = 0m;

                Decimal.TryParse(_geo["longitude"].ToString(), out _longitude);
                Decimal.TryParse(_geo["latitude"].ToString(), out _latitude);

                _request.Longitude = _longitude;
                _request.Latitude = _latitude;
            }

            return MapperAddress.ParseToDTO(_serviceAddress.Add(_request, request.DriverId));
        }

        public bool Update(AddressPutRequest request)
        {
            IAddress _request = MapperAddress.ParseToEntity(request);

            IDictionary<String, Decimal> _geo = _ApiGeocoding.GetGeoCordinates(request.AddressName + "," + request.Number + "," + request.City);

            if (_geo.ContainsKey("longitude") && _geo.ContainsKey("latitude"))
            {
                decimal _longitude = 0m;
                decimal _latitude = 0m;

                Decimal.TryParse(_geo["longitude"].ToString(), out _longitude);
                Decimal.TryParse(_geo["latitude"].ToString(), out _latitude);

                _request.Longitude = _longitude;
                _request.Latitude = _latitude;
            }

            return _serviceAddress.Update(_request);
        }

        public bool Delete(int id)
        {
            return _serviceAddress.Delete(id);
        }

        public AddressResponse Load(int DriverId)
        {
            return MapperAddress.ParseToDTO(_serviceAddress.Load(DriverId));
        }
        #endregion
    }
}
