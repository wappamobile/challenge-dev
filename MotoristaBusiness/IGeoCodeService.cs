using MotoristaEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MotoristaBusiness
{
    public interface IGeoCodeService
    {
        Task<GeoCode> GetGeoCode(string endereco);
    }
}
