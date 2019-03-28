using DriverRegistration.Domain.Services.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DriverRegistration.Domain.Services.Interfaces
{
    public interface IMapsService
    {
        Task<GeocodeResponseVO> GetCoordinates(string address);
    }
}
