using System.Linq;
using Wappa.Domain.Entities;
using Wappa.WebApi.ViewModels.Common;

namespace Wappa.WebApi.ViewModels.Response
{
    public class GetCidadesResponse : BaseResponse<CidadeViewModel>
    {
        public GetCidadesResponse(Cidade[] cidades)
        {
            this.Items = cidades.Select(x => new CidadeViewModel(x)).ToArray();
        }
    }
}
