using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;

namespace DriverLib.Api.Filters
{
    public class GetClaimsFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Thread.CurrentPrincipal = context.HttpContext.User;
            base.OnActionExecuting(context);
        }
    }
}
