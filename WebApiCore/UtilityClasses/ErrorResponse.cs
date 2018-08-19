using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace WebApiCore.UtilityClasses
{
    //eğer constructer da ExceptionContext yerine Exception gelirse constructur'ı overload yap.
    public class ErrorResponse
    {
        private string ErrorMessage { get; set; }
        private string ErrorAction { get; set; }
        private string ErrorController { get; set; }
        public ErrorResponse(ExceptionContext context)
        {
            this.ErrorAction = context.ActionDescriptor.DisplayName;
            this.ErrorController = context.RouteData.Values["controller"].ToString();
            this.ErrorMessage = context.Exception.ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new
            {
                ErrorMessage = ErrorMessage,
                ErrorAction = ErrorAction,
                ErrorController = ErrorController
            });
        }

    }
}
