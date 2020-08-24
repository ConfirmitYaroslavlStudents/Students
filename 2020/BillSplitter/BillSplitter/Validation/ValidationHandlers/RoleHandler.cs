using System.Linq;
using BillSplitter.Data;
using BillSplitter.HttpContextExtension;
using BillSplitter.Validation.ValidationMiddleware;
using Microsoft.Extensions.DependencyInjection;

namespace BillSplitter.Validation.ValidationHandlers
{
    public class RoleHandler
    {
        public bool Handle(ValidationContext context)
        {
            if (!context.HttpContext.Request.RouteValues.ContainsKey("billId"))
                return true;

            var billId = int.Parse(context.HttpContext.Request.RouteValues["billId"].ToString()!);

            var userId = context.HttpContext.GetCurrentUserId();

            var db = context.HttpContext.RequestServices.GetService<UnitOfWork>();

            var billMember = db.Bills
                .GetBillById(billId)
                .Members
                .FirstOrDefault(m => m.UserId == userId);

            if (billMember == null)
                return false;
            
            return context.Roles.Contains(billMember.Role);
        }
    }
}