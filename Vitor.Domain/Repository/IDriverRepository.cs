using System.Threading.Tasks;
using Vitor.Domain.Messages;
using Vitor.Domain.Messages.Request;
using Vitor.Domain.Messages.Response;
using Vitor.Domain.Model;

namespace Vitor.Domain.Repository
{
    public interface IDriverRepository
    {
        Task<GetDriverResponse> GetDriver(long id);
        Task<InsertDriverResponse> InsertDriver(InsertDriverRequest driver);
        Task InsertVehicle(Vehicle vehicle);
        Task<long> GetNewVehicleId();
        Task<UpdateDriverResponse> UpdatetDriver(UpdateDriverRequest driver);
        Task<DeleteDriverResponse> DeleteDriver(long id);
        Task<GetDriverResponse> Getdriverbyemail(string email);        
    }
}
