using DriverRegistration.Application.DTOs.Address;
using DriverRegistration.Domain.Factories;
using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Application.Mappers.Address
{
    public static class MapperAddress
    {
        #region Methods
        public static IAddress ParseToEntity(AddressPostRequest request)
        {
            return factoryAddress.Create(-1, request.AddressName, request.Number, request.Neighborhood, request.PostalCode, request.State, 0m, 0m, request.City);
        }

        public static IAddress ParseToEntity(AddressPutRequest request)
        {
            return factoryAddress.Create(request.Id, request.AddressName, request.Number, request.Neighborhood, request.PostalCode, request.State, 0m, 0m, request.City);
        }

        public static AddressResponse ParseToDTO(IAddress request)
        {
            if (request != null)
            {
                return new AddressResponse()
                {
                    AddressName = request.AddressName,
                    City = request.City,
                    Id = request.Id,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Neighborhood = request.Neighborhood,
                    Number = request.Number,
                    PostalCode = request.PostalCode,
                    State = request.State
                };
            }
            else
                return null;
        }
        #endregion
    }
}
