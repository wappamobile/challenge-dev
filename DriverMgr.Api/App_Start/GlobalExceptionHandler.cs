using DriverMgr.Business.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace DriverMgr.Api.App_Start
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        //A basic DTO to return back to the caller with data about the error
        private class ErrorInformation
        {
            public string Message { get; set; }

            public DateTime ErrorDate { get; set; }
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ValidationException)
            {
                var response = context.Request.CreateResponse(context.Exception.Message);

                response.StatusCode = HttpStatusCode.ExpectationFailed;

                throw new HttpResponseException(response);
            }
            
            base.Handle(context);
        }
    }
}