using System;
using System.Collections.Generic;
using System.Text;
using Teste.Servicos.Externos.DTO;

namespace Teste.Servicos.Externos
{
    public interface IServicoGoogleMaps
    {
        Coordenadas ObterCoordenadas(string endereco);
    }
}
