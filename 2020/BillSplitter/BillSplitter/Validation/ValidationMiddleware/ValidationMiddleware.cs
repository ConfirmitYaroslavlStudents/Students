using System.Net;
using System.Threading.Tasks;
using BillSplitter.Attributes;
using BillSplitter.Validation.ValidationHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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

        public Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var rolesAttribute = endpoint.Metadata.GetMetadata<RequireRolesAttribute>();
            
            if (rolesAttribute == null)
                return _next.Invoke(context);

            var validationContext = new ValidationContext(context, rolesAttribute.Roles);

            if (!_handler.Handle(validationContext))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            }

            return _next.Invoke(context);
        }
    }
}