using DriverRegistration.Application.DTOs.Car;
using DriverRegistration.Domain.Factories;
using DriverRegistration.Domain.Interfaces;

namespace DriverRegistration.Application.Mappers.Car
{
    public static class MapperCar
    {
        #region Methods
        public static ICar ParseToEntity(CarPostRequest request)
        {
            return factoryCar.Create(-1, request.Brand, request.Model, request.Plate);
        }

        public static ICar ParseToEntity(CarPutRequest request)
        {
            return factoryCar.Create(request.Id, request.Brand, request.Model, request.Plate);
        }

        public static CarResponse ParseToDTO(ICar request)
        {
            if (request != null)
            {
                return new CarResponse()
                {
                    Brand = request.Brand,
                    Id = request.Id,
                    Model = request.Model,
                    Plate = request.Plate
                };
            }
            else
                return null;
        }
        #endregion
    }
}
