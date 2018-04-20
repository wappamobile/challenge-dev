using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Domain.Services
{
    public interface ILocalizacaoGateway
    {
        Dictionary<string, string> ObterCoordenadas(string endereco);
    }
}
