using System.Linq;
using Wappa.Domain.Entities;
using Wappa.WebApi.ViewModels.Common;

namespace Wappa.WebApi.ViewModels.Response
{
    public class GetMotoristasResponse : BaseResponse<MotoristaViewModel>
    {
        public GetMotoristasResponse(Motorista[] motoristas)
        {
            this.Items = motoristas.Select(x => new MotoristaViewModel(x)).ToArray();
        }
    }
}
