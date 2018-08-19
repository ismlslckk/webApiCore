using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiCore.Loggers;

namespace WebApiCore.Filters
{
    public class ActionAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in context.ActionArguments)
            {
                stringBuilder.Append($"{item.Key}-{item.Value.ToString()},");
            }
            
            Logger.LogWriter(null);//bu sayede her isteğe gelen parametreleri loglayabiliriz.
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            int statusCode = context.HttpContext.Response.StatusCode;
            base.OnActionExecuted(context);
        }
    }
}
