using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Core.Services
{
    public interface IGeolocationService
    {
        Coordenadas ObterCoordenadas(String endereco);
    }
}
