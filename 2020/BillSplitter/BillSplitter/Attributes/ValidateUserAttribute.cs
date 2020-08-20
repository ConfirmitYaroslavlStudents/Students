using BillSplitter.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using BillSplitter.Data;

namespace BillSplitter.Attributes
{
    public class ValidateUserAttribute : ActionFilterAttribute
    {
        private UnitOfWork _db;
        public string Role { get; set; }

        public ValidateUserAttribute(UnitOfWork db)
        {
            _db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;

            var billId = (int) filterContext.ActionArguments["billId"];
            var bill = _db.Bills.GetBillById(billId);

            if (bill == null)
                filterContext.Result = controller.Error();

            var currentMember = bill.Members.Find(c => c.UserId == controller.GetUserId());

            if (currentMember == null)
                filterContext.Result = controller.Error();

            if (currentMember.Role != Role && currentMember.Role != "Admin")
                filterContext.Result = controller.Error();
        }
    }
}
