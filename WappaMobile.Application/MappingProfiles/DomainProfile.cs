using AutoMapper;
using WappaMobile.Domain;

namespace WappaMobile.Application
{
    /// <summary>
    /// Mapping profile for the domain.
    /// </summary>
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Driver, ModifyDriverDto>().ReverseMap();

            CreateMap<Driver, ViewDriverDto>()
                .ForMember(d => d.AddressLatitude, opt => opt.MapFrom(d => d.Address.Coordinates.Latitude))
                .ForMember(d => d.AddressLongitude, opt => opt.MapFrom(d => d.Address.Coordinates.Longitude));
        }
    }
}
