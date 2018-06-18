using ApplicationCore.Entity;
using ApplicationCore.Services.APIClient.Geocode.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IGeocodeService
    {
        void SetGeometryAsync(int enderecoId);
    }
}
