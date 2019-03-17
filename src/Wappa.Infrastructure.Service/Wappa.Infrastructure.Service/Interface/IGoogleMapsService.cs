using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wappa.Infrastructure.Service.Interface
{
    public interface IGoogleMapsService
    {
        Task<(double latitude, double longitude)> GetLatitudeLongitude(string address);
    }
}
