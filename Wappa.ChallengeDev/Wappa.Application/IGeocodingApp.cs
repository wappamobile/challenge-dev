using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;

namespace Wappa.Application
{
    public interface IGeocodingApp
    {

        Task<Localizacao> BuscarCoordenadasGeograficas(string endereco);
    }
}
