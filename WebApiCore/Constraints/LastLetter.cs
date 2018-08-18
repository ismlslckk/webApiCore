using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Constraints
{
    public class LastLetter : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string paramVal = values[routeKey].ToString().ToLower();

            if (paramVal.EndsWith("a") || paramVal.EndsWith("l"))
                return true;
            else
                return false;
        }
    }
}
