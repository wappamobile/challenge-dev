using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Models;

namespace Wappa.Business.Interfaces
{
    public interface ILocalizacaoBusiness
    {
        /// <summary>
        /// Retorna as coordenadas do endereço
        /// </summary>
        /// <param name="endereco">Endereço que deseja buscar as coordenadas</param>
        /// <returns></returns>
        Task<Localizacao> ObterCoordenadas(Endereco endereco);
    }
}
