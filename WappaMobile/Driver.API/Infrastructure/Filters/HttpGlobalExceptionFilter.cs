using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WappaMobile.Driver.API.Infrastructure.ActionResults;

namespace WappaMobile.Driver.API.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var json = new ErrorResponse
            {
                Messages = new[] { "An error ocurred." }
            };

            if (_env.IsDevelopment())
            {
                json.Exception = context.Exception;
            }

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        private class ErrorResponse
        {
            public string[] Messages { get; set; }

            public object Exception { get; set; }
        }
    }
}
