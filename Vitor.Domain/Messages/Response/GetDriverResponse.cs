using Vitor.Domain.Model;

namespace Vitor.Domain.Messages.Response
{
    public class GetDriverResponse : BaseResponse
    {
        public Driver Driver { get; set; }
    }
}
