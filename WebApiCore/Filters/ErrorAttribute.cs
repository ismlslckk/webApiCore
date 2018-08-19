using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiCore.Loggers;
using WebApiCore.UtilityClasses;

namespace WebApiCore.Filters
{
    public class ErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            //1.loglama
            Logger.LogWriter(context.Exception);

            //2.response
            ErrorResponse errorResponse = new ErrorResponse(context);
            context.Result = new ContentResult
            {
                Content = errorResponse.ToString(),
                StatusCode = 404
            };

        }
    }
}
