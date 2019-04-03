using AutoMapper;
using Wappa.Application.ViewModels;
using Wappa.Domain.Models;

namespace Wappa.Application.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Driver, DriverViewModel>();
        }
    }
}