using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;

namespace Wappa.DataAccess.Interfaces
{
    public interface IGoogleMapsClient
    {
        /// <summary>
        /// Retorna as Coordenadas(Latitude e Longitude) de um endereço
        /// </summary>
        /// <param name="endereco">Endereço que deseja obter as coordenadas</param>
        Task<Localizacao> ObterCoordenadas(Endereco endereco);
    }
}
