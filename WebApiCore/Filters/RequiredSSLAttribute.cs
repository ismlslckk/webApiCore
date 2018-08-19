using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiCore.Filters
{
    public class RequiredSSLAttribute : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsHttps)
            {
                filterContext.Result = new ContentResult
                {
                    Content = "SSL required",
                    StatusCode = 403
                };
            }
            else
            {
                base.OnAuthorization(filterContext);
            }

        }
      
    }
}
