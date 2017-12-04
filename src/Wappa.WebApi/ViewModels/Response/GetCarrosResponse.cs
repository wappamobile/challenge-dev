using System.Linq;
using Wappa.Domain.Entities;
using Wappa.WebApi.ViewModels.Common;

namespace Wappa.WebApi.ViewModels.Response
{
    public class GetCarrosResponse : BaseResponse<CarroViewModel>
    {
        public GetCarrosResponse(Carro[] carros)
        {
            this.Items = carros.Select(x => new CarroViewModel(x)).ToArray();
        }
    }
}
