using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Wappa.Middleware.Host.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected DefaultResult Result(IResult result)
        {
            return new DefaultResult(result);
        }

        protected IDataResult<T> ResultData<T>(IDataResult<T> result)
        {
            return result;
        }
    }
}
