using Wappa.Middleware.Domain.Configuration;
using Wappa.Middleware.EntityFrameworkCore.UoW;
using Microsoft.Extensions.Options;

namespace Wappa.Middleware.Core
{
    public class WappaMiddlewareServiceBase
    {
        public readonly IUnitOfWork _uow;
        public readonly IOptionsMonitor<GoogleMapsConfiguration> _options;

        public WappaMiddlewareServiceBase()
        {
        }

        public WappaMiddlewareServiceBase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public WappaMiddlewareServiceBase(IOptionsMonitor<GoogleMapsConfiguration> options)
        {
            _options = options;
        }
    }
}
