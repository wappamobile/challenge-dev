using AutoMapper;
using Wappa.Api.ExternalServices;
using Wappa.Api.Requests;
using Wappa.Api.Responses;

namespace Wappa.Api
{
	public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			this.CreateMap<CreateDriverRequest, DomainModel.Driver>().ReverseMap();
			this.CreateMap<CreateDriverRequest, DomainModel.Driver>().ForMember(driver => driver.Address, src => src.Ignore());

			this.CreateMap<UpdateDriverRequest, DomainModel.Driver>().ReverseMap();

			this.CreateMap<UpdateDriverAddressRequest, DomainModel.Address>().ReverseMap();
			this.CreateMap<UpdateDriverAddressRequest, Models.Address>().ReverseMap();

			this.CreateMap<UpdateDriverCarRequest, Models.Car>().ReverseMap();
			this.CreateMap<UpdateDriverCarRequest, DomainModel.Car>().ReverseMap();

			this.CreateMap<CreatedDriverResponse, DomainModel.Driver>().ReverseMap();

			this.CreateMap<Models.Driver, DomainModel.Driver>().ReverseMap();
			this.CreateMap<Models.Address, DomainModel.Address>().ReverseMap();
			this.CreateMap<Models.Car, DomainModel.Car>().ReverseMap();

			this.CreateMap<GoogleAddress, DomainModel.Address>()
				.ForMember(address => address.AddressLine, src => src.MapFrom(x => x.FormattedAddress))
				.ForMember(address => address.Latitude,	src => src.MapFrom(x => x.Location.Latitude))
				.ForMember(address => address.Longitude, src => src.MapFrom(x => x.Location.Longitude)).ReverseMap();
	
		}
    }
}
