using AutoMapper;
using Wappa.Application.ViewModels;
using Wappa.Domain.Commands;
using Wappa.Domain.Models;

namespace Wappa.Application.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<DriverViewModel, RegisterNewDriverCommand>()
               .ConstructUsing(c =>
               new RegisterNewDriverCommand(
                   c.Name, c.LastName, c.CarModel, c.CarBrand, c.CarPlate, c.Zipcode, c.Address, c.Coordinates));

            CreateMap<DriverViewModel, UpdateDriverCommand>()
                .ConstructUsing(c => new UpdateDriverCommand(
                    c.Id, c.Name, c.LastName, c.CarModel, c.CarBrand, c.CarPlate, c.Zipcode, c.Address, c.Coordinates));
        }
    }
}