using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.API.Models;
using Wappa.Infrastructure.Data.Models;

namespace Wappa.API.Mapper
{
    public class DomainProfiler : Profile
    {
        public DomainProfiler() 
        {
            CreateMap<DriverViewModel, Driver>()
                .ConstructUsing(c => new Driver(c.Id, c.FirstName, c.LastName, c.Address, c.Latitude, c.Longitude, 
                new Vehicle(c.Vehicle.Marca, c.Vehicle.Modelo, c.Vehicle.Placa)));
        }
    }
}
