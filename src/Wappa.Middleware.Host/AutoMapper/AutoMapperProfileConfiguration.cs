using AutoMapper;
using Wappa.Middleware.Application.Cars.Dto;
using Wappa.Middleware.Application.Drivers.Dto;
using Wappa.Middleware.Domain.Cars;
using Wappa.Middleware.Domain.Drivers;

namespace Wappa.Middleware.Miscellaneous
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<CarDto, Car>();
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<CreateOrEditCarDto, Car>();

            CreateMap<DriverDto, Driver>();
            CreateMap<Driver, DriverDto>().ReverseMap();
            CreateMap<CreateOrEditDriverDto, Driver>();
        }
    }
}
