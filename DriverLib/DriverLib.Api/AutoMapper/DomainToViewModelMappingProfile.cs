using AutoMapper;
using DriverLib.Api.ViewModels;
using DriverLib.Domain;
using DriverLib.Helper.Extensions;

namespace DriverLib.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile() : this("Profile") { }

        protected DomainToViewModelMappingProfile(string profileName) : base(profileName)
        {
            

            #region [ User ]
            CreateMap<User, UserVM>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            #endregion

            
        }
    }
}
