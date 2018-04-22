using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Business.Interfaces;
using Wappa.DataAccess.Interfaces;
using Wappa.Models;

namespace Wappa.Business.Implementations
{
    public class LocalizacaoBusiness : ILocalizacaoBusiness
    {
        private readonly IGoogleMapsClient googleMapsClient;

        public LocalizacaoBusiness(IGoogleMapsClient googleMapsClient)
        {
            this.googleMapsClient = googleMapsClient;
        }

        public Task<Localizacao> ObterCoordenadas(Endereco endereco)
        {
            return googleMapsClient.ObterCoordenadas(endereco);
        }
    }
}
