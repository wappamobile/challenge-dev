using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MG.WappaDriverAPI.Core.Models;
using MG.WappaDriverAPI.TransientObjects.Requests;
using MG.WappaDriverAPI.TransientObjects.Responses;

namespace MG.WappaDriverAPI.TransientObjects
{
    public static class MapExtension
    {
        public static void Int()
        {
            Mapper.Initialize(cfb =>
            {
                cfb.CreateMap<CarRequest, Car>();
                cfb.CreateMap<AddressRequest, Address>();
                cfb.CreateMap<DriverRequest, Driver>();
                cfb.ValidateInlineMaps = false;

                cfb.CreateMap<Car, CarRequest>();
                cfb.CreateMap<Address, AddressRequest>();
                cfb.CreateMap<Driver, DriverRequest>();

                cfb.CreateMap<Car, CarResponse>();
                cfb.CreateMap<Address, AddressResponse>();
                cfb.CreateMap<Driver, DriverResponse>();
            });
        }

        public static Driver ToDriver(this DriverRequest obj)
        {
            return Mapper.Map<DriverRequest, Driver>(obj);
        }

        public static DriverRequest ToDriverRequest(this Driver obj)
        {
            return Mapper.Map<Driver, DriverRequest>(obj);
        }


        public static Address ToAddress(this AddressRequest obj)
        {
            return Mapper.Map<AddressRequest, Address>(obj);
        }

        public static AddressRequest ToAddressRequest(this Address obj)
        {
            return Mapper.Map<Address, AddressRequest>(obj);
        }
        
        public static Driver ToDriver(this DriverResponse obj)
        {
            return Mapper.Map<DriverResponse, Driver>(obj);
        }

        public static DriverResponse ToDriverResponse(this Driver obj)
        {
            return Mapper.Map<Driver, DriverResponse>(obj);
        }


        public static Address ToAddress(this AddressResponse obj)
        {
            return Mapper.Map<AddressResponse, Address>(obj);
        }

        public static AddressResponse ToAddressResponse(this Address obj)
        {
            return Mapper.Map<Address, AddressResponse>(obj);
        }


    }
}
