using AutoMapper;
using Wappa.Challenge.Domain.Commands.Inputs;
using Wappa.Challenge.Domain.Models;

namespace Wappa.Challenge.Api
{
    /// <summary>
    /// Mapping profile between entities and commands
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingProfile()
        {
            CreateMap<CreateDriverCommand, Driver>()
                .ReverseMap();

            CreateMap<AddressCommand, Address>()
                .ReverseMap();

            CreateMap<CarCommand, Car>()
                .ReverseMap();
        }
    }
}