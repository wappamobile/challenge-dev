using Driver.Application.Data.Entities;
using Driver.Application.Services.Entities;
using System.Threading.Tasks;

namespace Driver.Application.Services.Interfaces
{
    public interface IGoogleApiService
    {
        Task<GoogleMapsResult> SearchAsync(AddressEntity addressEntity);
    }
}