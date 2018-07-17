using DriverRegistration.Application.DTOs.Driver;
using DriverRegistration.Domain.Factories;
using DriverRegistration.Domain.Interfaces;
using System.Collections.Generic;

namespace DriverRegistration.Application.Mappers.Driver
{
    public static class MapperDriver
    {
        #region Methods
        public static IDriver ParseToEntity(DriverPostRequest request)
        {
            return factoryDriver.Create(-1, request.FirstName, request.LastName);
        }

        public static IDriver ParseToEntity(DriverPutRequest request)
        {
            return factoryDriver.Create(request.Id, request.FirstName, request.LastName);
        }

        public static DriverResponse ParseToDTO(IDriver request)
        {
            if (request != null)
            {
                return new DriverResponse()
                {
                    Address = Address.MapperAddress.ParseToDTO(request.Address),
                    DriverCar = Car.MapperCar.ParseToDTO(request.DriverCar),
                    FirstName = request.FirstName,
                    Id = request.Id,
                    LastName = request.LastName
                };
            }
            else
                return null;
        }

        public static IEnumerable<DriverResponse> ParseToDTO(IEnumerable<IDriver> request)
        {
            List<DriverResponse> response = new List<DriverResponse>();

            if (request != null)
            {
                foreach (IDriver _driver in request)
                {
                    if (_driver != null)
                        response.Add(ParseToDTO(_driver));
                }
            }

            return response;
        }
        #endregion
    }
}
