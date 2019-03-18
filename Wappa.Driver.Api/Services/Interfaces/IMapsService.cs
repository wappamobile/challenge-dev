
using System.Threading.Tasks;
using Wappa.Driver.Api.Dtos;

namespace Wappa.Driver.Api.Services.Interfaces
{
    public interface IMapsService
    {
        /// <summary>
        /// Retorna a latitude e longitude de um endereço
        /// </summary>
        /// <param name="address">Endereço do motorista</param>
        /// <returns></returns>
        Task<MapDto> GetGeometry(DriverAddressDto address);
    }
}
