using BillSplitter.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BillSplitter.Validators
{
    public class ValidateUserAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var contr = filterContext.Controller as SuperController;

            if (!contr.CheckUserAccessToBill((int)filterContext.ActionArguments["billId"]))
            {
                filterContext.Result = contr.Error();
            }
        }
    }
}
