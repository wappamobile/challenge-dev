using System.Threading.Tasks;
using Wappa.Framework.Model.Comum;

namespace Wappa.Service.Geocoder
{
    public interface IGeocodingService
    {
        Task<Endereco> ObterLocalizacaoAsync();
    }
}
