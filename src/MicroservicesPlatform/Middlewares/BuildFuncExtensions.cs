using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesPlatform.Middlewares
{
    using BuildFunc = Action<Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>;

    public static class BuildFuncExtensions
    {
        public static BuildFunc UseLogging(this BuildFunc buildFunc, ILogger log)
        {
            buildFunc(next => CorrelationToken.Middleware(next, log));
            buildFunc(next => RequestLogging.Middleware(next, log));
            buildFunc(next => PerformanceLogging.Middleware(next, log));
            return buildFunc;
        }
    }
}
