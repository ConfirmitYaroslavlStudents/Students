using BillSplitter.Controllers;
using BillSplitter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
