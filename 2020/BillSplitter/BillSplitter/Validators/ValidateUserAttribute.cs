using BillSplitter.Controllers;
using Microsoft.AspNetCore.Http;
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

            var bill = contr.GetBillById((int)filterContext.ActionArguments["billId"]);

            if (bill==null || bill.UserId != contr.GetUserId())
            {
                filterContext.Result = contr.Error();
            }
        }
    }
}
