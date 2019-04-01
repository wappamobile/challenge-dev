using DotNetCore.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Wappa.Middleware.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
