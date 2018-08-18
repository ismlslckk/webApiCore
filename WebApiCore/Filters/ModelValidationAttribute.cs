using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCore.Filters
{
    public class ModelValidationAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                //SelectMany ile Errors property'si liste olarak geriye döndürür bu sayede daha sonra select diyerek sadece errormesage alanını seçebiliriz.
                var errorList = context.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList();
                string errorMessage = string.Join(Environment.NewLine, errorList);

                context.Result = new ContentResult
                {
                    Content = errorMessage,
                    StatusCode = 400
                };
            }
        }
    }
}
