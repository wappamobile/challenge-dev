using GoogleMaps.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMaps.WebApi.Services.Interfaces
{
    public interface IMapsService
    {
        Task<GeocodeResponse> GetGeocode(string address);
    }
}
