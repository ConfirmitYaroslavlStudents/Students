using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BillSplitter.Attributes;
using BillSplitter.Data;
using BillSplitter.Validation.ValidationHandlers;
using Microsoft.AspNetCore.Http;

namespace BillSplitter.Validation.ValidationMiddleware
{
    public class ValidationMiddleware 
    {
        private RequestDelegate _next;
        private RoleHandler _handler;
        
        public ValidationMiddleware(RequestDelegate next, RoleHandler roleHandler)
        {
            _next = next;
            _handler = roleHandler;
        }

        public Task Invoke(HttpContext context, UnitOfWork db)
        {
            var endpoint = context.GetEndpoint();
            var rolesAttribute = endpoint.Metadata.GetMetadata<RequireRolesAttribute>();
            
            if (rolesAttribute == null)
                return _next.Invoke(context);

            if (!context.Request.RouteValues.ContainsKey("billId"))
                return _next.Invoke(context);

            var billId = int.Parse(context.Request.RouteValues["billId"].ToString()!);

            var userId = int.Parse(
                context
                    .User.Claims
                    .Where(c => c.Type == "Id")
                    .Select(c => c.Value)
                    .SingleOrDefault() ?? string.Empty);

            var billMember = db.Bills
                .GetBillById(billId)
                .Members.FirstOrDefault(m => m.UserId == userId);

            if (!_handler.Handle(billMember, rolesAttribute.Roles))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            }
            return _next.Invoke(context);
        }
    }
}