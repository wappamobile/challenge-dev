using Microsoft.AspNetCore.Mvc;
using DriverLib.Jobs;

namespace DriverLib.Api.Controllers.Generic
{
    public class BaseJobController: Controller
    {
        protected IJobExecutor _executor;
        protected string _validToken;

        protected bool _IsValidJobToken() => (Request.Headers["Authorization"].ToString() == _validToken);
    }

    
}
