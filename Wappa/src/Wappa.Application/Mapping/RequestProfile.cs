using AutoMapper;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Models;

namespace Wappa.Application.Mapping
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<CreateDriverRequest, Driver>();
            CreateMap<ChangeDriverRequest, Driver>();
            CreateMap<RemoveDriverRequest, Driver>();
            CreateMap<QueryDriverRequest, Driver>();

            CreateMap<CreateVehicleRequest, Vehicle>();
            CreateMap<ChangeVehicleRequest, Vehicle>();
            CreateMap<RemoveVehicleRequest, Vehicle>();
            CreateMap<QueryVehicleRequest, Vehicle>();

            CreateMap<CreateAddressRequest, Address>();
            CreateMap<ChangeAddressRequest, Address>();
            CreateMap<RemoveAddressRequest, Address>();
            CreateMap<QueryAddressRequest, Address>();
        }
    }
}