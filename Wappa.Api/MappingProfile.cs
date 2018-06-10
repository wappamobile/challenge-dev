using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Api.DomainModel;
using Wappa.Api.Requests;

namespace Wappa.Api
{
    public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			this.CreateMap<CreateDriverRequest, Driver>().ReverseMap();
			this.CreateMap<Models.Driver, Driver>().ReverseMap();
			this.CreateMap<Models.Address, Address>().ReverseMap();
			this.CreateMap<Models.Car, Car>().ReverseMap();
		}
    }
}
