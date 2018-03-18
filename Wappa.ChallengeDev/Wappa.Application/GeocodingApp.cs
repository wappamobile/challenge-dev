using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.DataAccess.Contracts;
using Wappa.Models;

namespace Wappa.Application
{
    public class GeocodingApp : IGeocodingApp
    {
        private IGeocoding proxy;
        public GeocodingApp(IGeocoding proxy)
        {
            this.proxy = proxy;
        }

        public Task<Localizacao> BuscarCoordenadasGeograficas(string endereco)
        {
            return proxy.BuscarCoordenadasGeograficas(endereco);
        }
    }
}
