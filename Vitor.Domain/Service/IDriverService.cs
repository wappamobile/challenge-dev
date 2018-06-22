using System.Threading.Tasks;
using Vitor.Domain.Messages;
using Vitor.Domain.Messages.Request;
using Vitor.Domain.Messages.Response;

namespace Vitor.Domain.Service
{
    public interface IDriverService 
    {
        Task<GetDriverResponse> GetDriver(long id);
        Task<InsertDriverResponse> InsertDriver(InsertDriverRequest driver);
        Task<UpdateDriverResponse> UpdateDriver(UpdateDriverRequest driver);
        Task<DeleteDriverResponse> DeleteDriver(long id);
        Task<GetDriverResponse> Getdriverbyemail(string email);
    }
}
