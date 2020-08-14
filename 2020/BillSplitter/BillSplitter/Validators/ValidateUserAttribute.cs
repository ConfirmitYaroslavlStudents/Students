using BillSplitter.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using BillSplitter.Data;

namespace BillSplitter.Validators
{
    public class ValidateUserAttribute : ActionFilterAttribute
    {
        private UnitOfWork _db;
        public ValidateUserAttribute(UnitOfWork db)
        {
            _db = db;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            var bill = _db.Bills.GetBillById((int)filterContext.ActionArguments["billId"]);

            if (bill == null || bill.UserId != controller.GetUserId())
                filterContext.Result = controller.Error();
        }
    }
}
