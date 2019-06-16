using LibOwin;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesPlatform.Middlewares
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public static class RequestLogging
    {
        public static AppFunc Middleware(AppFunc next, ILogger log)
        {
            return async env =>
            {
                var owinContext = new OwinContext(env);
                log.Debug("Incoming request: {Method}, {Uri}, {Query}, {@Headers}",
                          owinContext.Request.Uri.GetLeftPart(UriPartial.Path),
                          owinContext.Request.Uri.Query.Replace("?", string.Empty),
                          owinContext.Request.Headers);
                await next(env);
                log.Debug("Outgoing response: {StatusCode}, {@Headers}",
                          owinContext.Response.StatusCode,
                          owinContext.Response.Headers);
            };
        }
    }

    public static class PerformanceLogging
    {
        public static AppFunc Middleware(AppFunc next, ILogger log)
        {
            return async env =>
            {
                var stopWatch = Stopwatch.StartNew();
                await next(env).ConfigureAwait(false);
                stopWatch.Stop();
                var owinContext = new OwinContext(env);
                var uri = new
                {
                    Path = owinContext.Request.Uri.GetLeftPart(UriPartial.Path),
                    Query = owinContext.Request.Uri.Query.Replace("?", string.Empty)
                };
                log.Information("Request: {Method} {@Uri} executed in {RequestTime}ms with HTTP Status {StatusCode}",
                                owinContext.Request.Method,
                                uri,
                                stopWatch.ElapsedMilliseconds,
                                owinContext.Response.StatusCode);
            };
        }
    }

    public static class CorrelationToken
    {
        public static AppFunc Middleware(AppFunc next, ILogger log)
        {
            return async env =>
            {
                var owinContext = new OwinContext(env);
                var key = LogContextTypes.CORRELATION_TOKEN;
                var correlationToken = owinContext.Request.Headers[key] ?? Guid.NewGuid().ToString("N");
                owinContext.Set(key, correlationToken);

                using (LogContext.PushProperty("CorrelationToken", correlationToken))
                {
                    try
                    {
                        await next(env).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex, "Unhandled exception");
                        throw;
                    }
                }
            };
        }
    }
}
