using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;

namespace Wappa.DataAccess.Contracts
{
    public interface IGeocoding
    {
        Task<Localizacao> BuscarCoordenadasGeograficas(string endereco);
    }
}
